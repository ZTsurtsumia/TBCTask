namespace PersonDirectory.Api.Controllers.Persons
{
    public class PictureUploadRequest
    {
        public int PersonId { get; set; }
        public IFormFile Picture { get; set; }

    }
}
