namespace PersonDirectory.Domain.Persons
{
    public class ConnectedPerson
    {
        public ConnectionType Type { get; }
        public int ConnectedPersonId { get; }

        public ConnectedPerson(ConnectionType type, int connectedPersonId)
        {
            Type = type;
            ConnectedPersonId = connectedPersonId;
        }

        public override string ToString() => $"{Type}: {ConnectedPersonId}";
    }
}

