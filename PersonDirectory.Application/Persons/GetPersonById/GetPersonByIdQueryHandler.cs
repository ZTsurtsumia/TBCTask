using Microsoft.EntityFrameworkCore;
using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Application.Data;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;

namespace PersonDirectory.Application.Persons.GetPersonById
{
    internal sealed class GetPersonByIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetPersonByIdQuery, PersonByIdResponse>
    {
        public async Task<Result<PersonByIdResponse>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await context.Persons
            .Where(u => u.Id == request.PersonId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

                if (person == null)
                    return Result.Failure<PersonByIdResponse>(PersonErrors.NotFound);

                PersonByIdResponse user = new()
                {
                    Id = person.Id,
                    FirstName = person.FirstName.Value,
                    LastName = person.LastName.Value,
                    Phones = person.MobilePhones ?? [],
                    ConnectedPeople = person.ConnectedPersons ?? []
                };

                return Result.Success(user);
            }
            catch (Exception ex)
            {
                return Result.Failure<PersonByIdResponse>(new Error(ErrorList.General, ex.Message));
            }
        }
    }
}
