using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Application.Persons.GetGroupedCP;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.UpdatePerson
{
    internal class UpdatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdatePersonCommand>
    {
        public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await personRepository.GetByIdAsync(request.Id, cancellationToken);
            if (person == null)
                return Result.Failure<GetGroupedCPResponse>(PersonErrors.NotFound);

            person.UpdatePerson(
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                request.Sex,
                new PersonalN(request.PersonalN),
                new DateOfBirth(request.DateOfBirth),
                new City(request.City),
                [.. request.MobilePhones]
                );

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
