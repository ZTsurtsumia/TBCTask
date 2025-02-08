using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.SearchPerson
{
    public class SearchPersonResponse
    {
        public List<Person> Person { get; set; } = [];
        public int Count { get; set; }
    }
}
