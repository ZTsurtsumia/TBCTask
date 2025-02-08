using PersonDirectory.Application.Abstractions.Messaging;

namespace PersonDirectory.Application.Persons.UpdatePicture
{
    public record UpdatePictureCommand(
        int Id,
        byte[] FileData,
        string FileName) : ICommand;
}
