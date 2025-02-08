using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.UpdateConnectedPersons
{
    public record UpdateConnectedPersonsCommand(
        int PersonId,
        ConnectedPerson[] ConnectedPeople) : ICommand;
}
