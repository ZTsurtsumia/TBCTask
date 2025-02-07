using PersonDirectory.Domain.Abstractions;

namespace PersonDirectory.Domain.Persons
{
    public sealed class Person : Entity
    {
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Sex Sex { get; private set; }
        public PersonalN PersonalN { get; private set; }
        public DateOfBirth DateOfBirth { get; private set; }
        public City City { get; private set; }
        public List<MobilePhone>? MobilePhones { get; private set; }
        public Picture? Picture { get; private set; }
        public List<ConnectedPerson>? ConnectedPersons { get; private set; }

        private Person()
        {

        }
        private Person(int id,
            FirstName firstName,
            LastName lastName,
            Sex sex,
            PersonalN personalN,
            DateOfBirth dateOfBirth,
            City city,
            List<MobilePhone> mobilePhones,
            List<ConnectedPerson> connectedPersons) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            PersonalN = personalN;
            DateOfBirth = dateOfBirth;
            City = city;
            MobilePhones = mobilePhones;
            ConnectedPersons = connectedPersons;
        }

        public static Person CreatePerson(
        FirstName firstName,
        LastName lastName,
        Sex sex,
        PersonalN personalN,
        DateOfBirth dateOfBirth,
        City city,
        List<MobilePhone>? mobilePhones,
        List<ConnectedPerson>? connectedPersons)
        {
            return new Person(
                0,
                firstName,
                lastName,
                sex,
                personalN,
                dateOfBirth,
                city,
                mobilePhones,
                connectedPersons
            );
        }

        public void UpdatePerson(
         FirstName firstName,
         LastName lastName,
         Sex sex,
         PersonalN personalN,
         DateOfBirth dateOfBirth,
         City city,
         List<MobilePhone> mobilePhones)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            PersonalN = personalN;
            DateOfBirth = dateOfBirth;
            City = city;
            MobilePhones = mobilePhones;
        }

        public void UpdatePicture(Picture picture)
        {
            Picture = picture;
        }

        public void UpdateConnectedPersons(List<ConnectedPerson> connectedPersons)
        {
            ConnectedPersons = connectedPersons;
        }

        public Dictionary<string, int> GetGroupedConnectedPersons(ConnectionType? type = null)
        {
            return ConnectedPersons
                .GroupBy(dto => dto.Type)
                 .Where(group => !type.HasValue || group.Key == type.Value)
                .ToDictionary(group => group.Key.ToString(), group => group.Count());
        }
    }
}

