using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    public class MobilePhoneDTO
    {
        public MobilePhoneType Type { get; set; }
        public string Number { get; set; }
    }
}
