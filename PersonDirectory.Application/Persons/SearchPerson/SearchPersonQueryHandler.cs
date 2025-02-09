using Microsoft.EntityFrameworkCore;
using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Application.Data;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.SearchPerson
{
    internal class SearchPersonQueryHandler(IApplicationDbContext context) : IQueryHandler<SearchPersonQuery, SearchPersonResponse>
    {
        public async Task<Result<SearchPersonResponse>> Handle(SearchPersonQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Person> query = context.Persons.AsQueryable();

                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    string searchTerm = request.SearchTerm.ToLower();

                    query = query.Where(p =>
                        p.FirstName.Value.ToLower().Contains(searchTerm) ||
                        p.LastName.Value.ToLower().Contains(searchTerm) ||
                        p.PersonalN.Value.Contains(searchTerm));
                }
                else
                {
                    if (!string.IsNullOrEmpty(request.FirstName))
                        query = query.Where(p => p.FirstName.Value.Contains(request.FirstName));

                    if (!string.IsNullOrEmpty(request.LastName))
                        query = query.Where(p => p.LastName.Value.Contains(request.LastName));

                    if (request.Sex.HasValue)
                        query = query.Where(p => p.Sex == request.Sex);

                    if (!string.IsNullOrEmpty(request.PersonalN))
                        query = query.Where(p => p.PersonalN.Value == request.PersonalN);

                    if (request.DateOfBirth.HasValue)
                        query = query.Where(p => p.DateOfBirth.Value == request.DateOfBirth.Value);

                    if (!string.IsNullOrEmpty(request.City))
                        query = query.Where(p => p.City.Value.Contains(request.City));
                }

                int totalCount = await query.CountAsync(cancellationToken: cancellationToken);

                List<Person> people = await query
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken: cancellationToken);

                var result = new SearchPersonResponse()
                {
                    Person = people,
                    Count = totalCount,
                };

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<SearchPersonResponse>(new Error(ErrorList.General, ex.Message));
            }
        }

    }
}
