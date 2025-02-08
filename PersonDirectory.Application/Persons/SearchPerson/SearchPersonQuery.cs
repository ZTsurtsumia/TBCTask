using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.SearchPerson
{
    public record SearchPersonQuery(
        string? SearchTerm,
        string? FirstName,
        string? LastName,
        Sex? Sex,
        string? PersonalN,
        DateTime? DateOfBirth,
        string? City,
        int Page,
        int PageSize
        ) : IQuery<SearchPersonResponse>;
}
