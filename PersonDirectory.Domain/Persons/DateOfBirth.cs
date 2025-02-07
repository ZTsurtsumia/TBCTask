namespace PersonDirectory.Domain.Persons
{
    public record DateOfBirth
    {
        public DateTime Value { get; }

        public DateOfBirth(DateTime value)
        {
            if (value > DateTime.UtcNow.AddYears(-18))
                throw new ArgumentException("Person must be at least 18 years old.");

            Value = value;
        }
        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}
