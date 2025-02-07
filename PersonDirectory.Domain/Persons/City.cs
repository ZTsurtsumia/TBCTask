namespace PersonDirectory.Domain.Persons
{
    public record City
    {
        public string Value { get; }

        public City(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("City cannot be empty.");

            if (!Cities.Contains(value))
                throw new ArgumentException($"City '{value}' is not valid.");

            Value = value;
        }
        public override string ToString() => Value;

        private static readonly IReadOnlyCollection<string> Cities = [
     "Tbilisi",
     "Sokhumi",
     "Rustavi",
     "Batumi"
    ];
    }

}
