using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Application.Persons.GetPersonById;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.DeletePerson
{
    internal class DeletePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeletePersonCommand>
    {
        public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await personRepository.GetByIdAsync(request.Id, cancellationToken);
                if (person == null)
                    return Result.Failure<PersonByIdResponse>(PersonErrors.NotFound);

                //We wont do that in RealTime
                personRepository.Delete(person);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(ErrorList.General, ex.Message));
            }
        }
    }
}
