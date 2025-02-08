using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Dtos
{
    public static class DTOExtensions
    {
        public static MobilePhone[]? MoveToDomain(this MobilePhoneDTO[] dtoPhones)
        {
            return dtoPhones?.Select(dto => new MobilePhone(dto.Type, dto.Number)).ToArray() ?? null;
        }

        public static ConnectedPerson[]? MoveToDomain(this ConnectedPersonDTO[] dtoPhones)
        {
            return dtoPhones?.Select(dto => new ConnectedPerson(dto.Type, dto.ConnectedPersonId)).ToArray() ?? null;
        }
    }

}
