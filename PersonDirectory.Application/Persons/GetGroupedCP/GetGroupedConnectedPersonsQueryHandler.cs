using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.GetGroupedCP
{
    internal class GetGroupedConnectedPersonsQueryHandler(IPersonRepository personRepository) : IQueryHandler<GetGroupedConnectedPersonsQuery, GetGroupedCPResponse>
    {
        public async Task<Result<GetGroupedCPResponse>> Handle(GetGroupedConnectedPersonsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await personRepository.GetByIdAsync(request.PersonId, cancellationToken);

                if (person == null)
                    return Result.Failure<GetGroupedCPResponse>(PersonErrors.NotFound);

                var response = new GetGroupedCPResponse
                {
                    Response = person.GetGroupedConnectedPersons(request.Type)
                };

                return Result.Success(response);
            }
            catch (Exception ex)
            {
                return Result.Failure<GetGroupedCPResponse>(new Error(ErrorList.General, ex.Message));
            }
        }
    }
}
