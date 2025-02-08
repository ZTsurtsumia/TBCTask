using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    public class GroupedConnectedPersonRequest
    {
        public int Id { get; set; }
        public ConnectionType? Type { get; set; }
    }
}
