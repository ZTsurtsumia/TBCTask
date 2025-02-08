using PersonDirectory.Application.Abstractions.Messaging;

namespace PersonDirectory.Application.Persons.GetPersonById
{
    public record GetPersonByIdQuery(int PersonId) : IQuery<PersonByIdResponse>;
}
