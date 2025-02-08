using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Persons.GetGroupedCP
{
    public record GetGroupedConnectedPersonsQuery(int PersonId, ConnectionType? Type) : IQuery<GetGroupedCPResponse>;
}
