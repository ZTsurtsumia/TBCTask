using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.GetPersonById
{
    public class PersonByIdResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<MobilePhone> Phones { get; set; }

        public List<ConnectedPerson> ConnectedPeople { get; set; }

    }
}