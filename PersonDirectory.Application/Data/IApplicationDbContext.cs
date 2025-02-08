using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Person> Persons { get; }
    }
}
