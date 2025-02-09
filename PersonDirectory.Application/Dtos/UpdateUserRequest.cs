using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Sex Sex { get; set; }
        public required string PersonalN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string City { get; set; }
        public MobilePhoneDTO[]? MobilePhones { get; set; }
    }
}
