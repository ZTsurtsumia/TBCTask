using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Domain.UnitTests.Person
{
    internal static class PersonData
    {
        public static readonly FirstName FirstName = new("First");
        public static readonly LastName LastName = new("Last");
        public static readonly City City = new("Sokhumi");
        public static readonly DateOfBirth DateOfBirth = new(new DateTime(2000, 6, 17));
        public static readonly PersonalN personalN = new("12343546576");
        public static readonly Sex Sex = Sex.Male;
    }
}
