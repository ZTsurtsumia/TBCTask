namespace PersonDirectory.Domain.Persons
{
    public record Picture
    {
        public string Value { get; }

        public Picture(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Picture URL cannot be empty.");

            Value = value;
        }
        public override string ToString() => Value;
    }
}
