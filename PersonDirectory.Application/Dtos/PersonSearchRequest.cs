using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    public class PersonSearchRequest
    {
        public string? SearchTerm { get; set; }  // Quick search (Name, Surname, PersonalN)
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Sex? Sex { get; set; }
        public string? PersonalN { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? City { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
