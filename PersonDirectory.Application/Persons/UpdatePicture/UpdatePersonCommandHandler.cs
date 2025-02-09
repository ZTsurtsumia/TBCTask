using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Application.Persons.GetGroupedCP;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.UpdatePicture;

internal class UpdatePictureCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdatePictureCommand>
{
    public async Task<Result> Handle(UpdatePictureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var person = await personRepository.GetByIdAsync(request.Id, cancellationToken);
            if (person == null)
                return Result.Failure<GetGroupedCPResponse>(PersonErrors.NotFound);

            if (request.FileData == null || request.FileData.Length == 0)
            {
                return Result.Failure(new Error(ErrorList.General, "No File Provided"));
            }
            string _pictureFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Pictures");
            if (!Directory.Exists(_pictureFolderPath))
            {
                Directory.CreateDirectory(_pictureFolderPath);
            }

            if (!string.IsNullOrWhiteSpace(person?.Picture?.Value))
            {
                File.Delete(person.Picture.Value);
            }

            string fileName = $"{DateTime.Now.ToFileTime()}_{person?.Id}";
            string filePath = Path.Combine(_pictureFolderPath, fileName);


            await File.WriteAllBytesAsync(filePath, request.FileData, cancellationToken);

            person?.UpdatePicture(
                    new Picture(filePath)
                );

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {

            return Result.Failure(new Error(ErrorList.General, ex.Message));
        }

    }
}
