using System.Text.RegularExpressions;

namespace PersonDirectory.Domain.Persons
{
    public record FirstName
    {
        public string Value { get; }

        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50)
                throw new ArgumentException("First name must be between 2 and 50 characters.");

            bool isGeorgian = Regex.IsMatch(value, "^[\\u10A0-\\u10FF]+$");
            bool isLatin = Regex.IsMatch(value, "^[A-Za-z]+$");

            if (!(isGeorgian ^ isLatin)) // Ensures only one script is used
                throw new ArgumentException("First name must contain only Georgian or only Latin letters.");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
