using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public string PersonalN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public MobilePhoneDTO[] MobilePhones { get; set; }
    }
}
