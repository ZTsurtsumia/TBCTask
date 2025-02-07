namespace PersonDirectory.Domain.Abstractions
{
    public abstract class Entity
    {
        public int Id { get; init; }

        public Entity(int id)
        {
            Id = id;
        }
        protected Entity()
        {

        }
    }

}
