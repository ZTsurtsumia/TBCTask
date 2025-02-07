namespace PersonDirectory.Domain.Persons
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        void Add(Person person);
        void Delete(Person person);
        Task<bool> ConnectedPersonExist(List<int>? ids);

    }
}
