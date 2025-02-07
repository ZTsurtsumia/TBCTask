namespace PersonDirectory.Domain.Persons
{
    public class MobilePhone
    {
        public MobilePhoneType Type { get; }
        public string Number { get; }
        public MobilePhone(MobilePhoneType type, string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number.Length < 4 || number.Length > 50)
                throw new ArgumentException("Phone number must be between 4 and 50 characters.");

            Type = type;
            Number = number;
        }
        public override string ToString() => $"{Type}: {Number}";
    }
}

