using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Application.Persons.GetGroupedCP;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.UpdateConnectedPersons
{
    internal class UpdateConnectedPersonsCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateConnectedPersonsCommand>
    {
        public async Task<Result> Handle(UpdateConnectedPersonsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await personRepository.GetByIdAsync(request.PersonId, cancellationToken);
                if (person == null)
                    return Result.Failure<GetGroupedCPResponse>(PersonErrors.NotFound);

                var connectedPersonIds = request.ConnectedPeople?.Select(cp => cp.ConnectedPersonId).ToList();

                if (connectedPersonIds != null && connectedPersonIds?.Count != 0)
                {
                    if (!await personRepository.ConnectedPersonExist(connectedPersonIds))
                    {
                        return Result.Failure<int>(PersonErrors.ConnectedPersonNotExist);
                    }
                }

                person.UpdateConnectedPersons([.. request.ConnectedPeople]);
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
