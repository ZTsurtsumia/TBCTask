using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    public class ConnectedPersonDTO
    {
        public ConnectionType Type { get; set; }
        public int ConnectedPersonId { get; set; }
    }
}
