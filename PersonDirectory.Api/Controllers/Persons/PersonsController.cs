using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Api.Extensions;
using PersonDirectory.Api.Filters;
using PersonDirectory.Application.Dtos;
using PersonDirectory.Application.Persons.AddPerson;
using PersonDirectory.Application.Persons.DeletePerson;
using PersonDirectory.Application.Persons.GetGroupedCP;
using PersonDirectory.Application.Persons.GetPersonById;
using PersonDirectory.Application.Persons.SearchPerson;
using PersonDirectory.Application.Persons.UpdateConnectedPersons;
using PersonDirectory.Application.Persons.UpdatePerson;
using PersonDirectory.Application.Persons.UpdatePicture;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Api.Controllers.Persons
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController(ISender sender) : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var query = new GetPersonByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(SuccessResponse(result.Value)) : result.Error.HandleError();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelStateActionFilter))]
        public async Task<IActionResult> Create(AddPersonRequest request, CancellationToken cancellationToken)
        {
            ConnectedPerson[] connectedPerson = request.ConnectedPersons.MoveToDomain();
            MobilePhone[] phones = request.MobilePhones.MoveToDomain();

            var command = new AddPersonCommand(
                request.FirstName,
                request.LastName,
                request.Sex,
                request.PersonalN,
                request.DateOfBirth,
                request.City,
                phones,
                connectedPerson
                );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(SuccessResponse(result.Value)) : result.Error.HandleError();

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            MobilePhone[] phones = request.MobilePhones.MoveToDomain();

            var command = new UpdatePersonCommand(
                request.Id,
                request.FirstName,
                request.LastName,
                request.Sex,
                request.PersonalN,
                request.DateOfBirth,
                request.City,
                phones
                );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : result.Error.HandleError();

        }

        [HttpPut("picture")]
        public async Task<IActionResult> PictureUpload([FromForm] PictureUploadRequest request, CancellationToken cancellationToken)
        {
            if (request.Picture == null || request.Picture.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var memoryStream = new MemoryStream();
            await request.Picture.CopyToAsync(memoryStream, cancellationToken);
            var fileData = memoryStream.ToArray();
            var fileName = Path.GetFileName(request.Picture.FileName);

            var command = new UpdatePictureCommand(request.PersonId, fileData, fileName);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : result.Error.HandleError();

        }

        [HttpGet("connectedPersonsReport")]
        public async Task<IActionResult> GetGroupedPersonsReport([FromQuery] GroupedConnectedPersonRequest request, CancellationToken cancellationToken)
        {

            var query = new GetGroupedConnectedPersonsQuery(
                request.Id,
                request.Type
                );

            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(SuccessResponse(result.Value)) : result.Error.HandleError();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeletePersonCommand(id);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : result.Error.HandleError();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPersons([FromQuery] PersonSearchRequest request, CancellationToken cancellationToken)
        {
            var query = new SearchPersonQuery(
                request.SearchTerm,
                request.FirstName,
                request.LastName,
                request.Sex,
                request.PersonalN,
                request.DateOfBirth,
                request.City,
                request.Page,
                request.PageSize
            );

            var result = await sender.Send(query, cancellationToken);


            return result.IsSuccess ? Ok(new
            {
                TotalCount = result.Value.Count,
                request.Page,
                request.PageSize,
                Data = result.Value.Person
            }) : result.Error.HandleError();
        }

        [HttpPut("connectedPersons")]
        public async Task<IActionResult> UpdateConnectedPersons(UpdateConnectedPersonRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateConnectedPersonsCommand(request.PersonId, request.ConnectedPeople.MoveToDomain());

            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : result.Error.HandleError();

        }
    }
}
