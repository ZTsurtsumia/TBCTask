using System.Text.RegularExpressions;

namespace PersonDirectory.Domain.Persons
{
    public record PersonalN
    {
        public string Value { get; }

        public PersonalN(string value)
        {
            if (!Regex.IsMatch(value, @"^\d{11}$"))
                throw new ArgumentException("PersonalN must be exactly 11 digits.");

            Value = value;
        }
        public override string ToString() => Value;
    }
}
