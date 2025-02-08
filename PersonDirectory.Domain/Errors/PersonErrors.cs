using PersonDirectory.Domain.Abstractions;

namespace PersonDirectory.Domain.Errors
{
    public static class PersonErrors
    {
        public static readonly Error NotFound = new(
            ErrorList.PersonNotFound,
            "The Person with the specified id was not found");

        public static readonly Error ConnectedPersonNotExist = new(
            ErrorList.ConnectedPersonNotExist,
            "No User was found with ConnectedPersonId");
    }
}

