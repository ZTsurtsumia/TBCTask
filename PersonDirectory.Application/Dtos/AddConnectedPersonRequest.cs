
namespace PersonDirectory.Application.Dtos
{
    public class UpdateConnectedPersonRequest
    {
        public int PersonId { get; set; }
        public ConnectedPersonDTO[] ConnectedPeople { get; set; }
    }
}
