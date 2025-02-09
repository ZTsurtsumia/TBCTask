using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.AddPerson
{
    internal sealed class AddPersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<AddPersonCommand, int>
    {
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var connectedPersonIds = request.ConnectedPersons?.Select(cp => cp.ConnectedPersonId).ToList();

                if (connectedPersonIds != null && connectedPersonIds?.Count != 0)
                {
                    if (!await _personRepository.ConnectedPersonExist(connectedPersonIds))
                    {
                        return Result.Failure<int>(PersonErrors.ConnectedPersonNotExist);
                    }
                }

                var person = Person.CreatePerson(
                    new FirstName(request.FirstName),
                    new LastName(request.LastName),
                    request.Sex,
                    new PersonalN(request.PersonalN),
                    new DateOfBirth(request.DateOfBirth),
                    new City(request.City),
                    [.. (request.MobilePhones ?? [])],
                    [.. (request.ConnectedPersons ?? [])]
                    );

                _personRepository.Add(person);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success(person.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(ErrorList.General, ex.Message));
            }
        }
    }
}
