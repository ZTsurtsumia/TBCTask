using System.Text.RegularExpressions;

namespace PersonDirectory.Domain.Persons
{
    public record LastName
    {
        public string Value { get; }

        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Last name must be between 2 and 50 characters.");

            bool isGeorgian = Regex.IsMatch(value, "^[\u10A0-\u10FF]+$");
            bool isLatin = Regex.IsMatch(value, "^[A-Za-z]+$");

            if (!(isGeorgian ^ isLatin))
                throw new ArgumentException("Last name must contain only Georgian or only Latin letters.");

            Value = value;
        }
    }
}
