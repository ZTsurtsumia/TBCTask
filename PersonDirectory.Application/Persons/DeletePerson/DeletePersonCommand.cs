using PersonDirectory.Application.Abstractions.Messaging;

namespace PersonDirectory.Application.Persons.DeletePerson
{
    public record DeletePersonCommand(
        int Id) : ICommand;
}
