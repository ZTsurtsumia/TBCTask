using FluentAssertions;

namespace PersonDirectory.Domain.UnitTests.Person
{
    public class PersonTest
    {
        [Fact]
        public void Create_Should_SetPropertyValue()
        {
            // Act
            var person = Persons.Person.CreatePerson(PersonData.FirstName, PersonData.LastName, PersonData.Sex, PersonData.personalN, PersonData.DateOfBirth,
                                                    PersonData.City, null, null);

            // Assert
            person.FirstName.Should().Be(PersonData.FirstName);
            person.LastName.Should().Be(PersonData.LastName);
            person.PersonalN.Should().Be(PersonData.personalN);
        }
    }
}
