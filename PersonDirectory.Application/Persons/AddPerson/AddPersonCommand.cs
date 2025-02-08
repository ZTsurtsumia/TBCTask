using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.AddPerson
{
    public record AddPersonCommand(
        string FirstName,
        string LastName,
        Sex Sex,
        string PersonalN,
        DateTime DateOfBirth,
        string City,
        MobilePhone[]? MobilePhones,
        ConnectedPerson[]? ConnectedPersons) : ICommand<int>;
}
