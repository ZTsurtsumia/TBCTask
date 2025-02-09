using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class PersonRepository(ApplicationDbContext dbContext) : Repository<Person>(dbContext), IPersonRepository
    {
        public async Task<bool> ConnectedPersonExist(List<int>? ids)
        {
            if (ids == null || ids.Count == 0)
                return false;

            var existingCount = await dbContext.Persons
                .Where(p => ids.Contains(p.Id))
                .CountAsync();

            return existingCount == ids.Count;
        }
    }
}
