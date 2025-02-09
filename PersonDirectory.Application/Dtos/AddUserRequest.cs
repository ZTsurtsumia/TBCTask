using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    /// <summary>
    /// Represents a request to add a new person.
    /// </summary>
    public class AddPersonRequest
    {
        /// <summary>
        /// The first name of the person.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// The last name of the person.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// The gender of the person.
        /// 0 - Male
        /// 1 - Female
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// The personal identification number of the person.
        /// </summary>
        public required string PersonalN { get; set; }

        /// <summary>
        /// The date of birth of the person.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The city where the person resides.
        ///  "Tbilisi",
        /// "Sokhumi",
        /// "Rustavi",
        /// "Batumi"
        /// </summary>
        public required string City { get; set; }

        public MobilePhoneDTO[]? MobilePhones { get; set; }
        public ConnectedPersonDTO[]? ConnectedPersons { get; set; }
    }
}
