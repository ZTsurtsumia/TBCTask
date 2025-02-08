using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.UpdatePerson
{
    public record UpdatePersonCommand(
        int Id,
        string FirstName,
        string LastName,
        Sex Sex,
        string PersonalN,
        DateTime DateOfBirth,
        string City,
        MobilePhone[] MobilePhones) : ICommand;
}
