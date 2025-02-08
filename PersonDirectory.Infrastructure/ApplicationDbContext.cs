using Microsoft.EntityFrameworkCore;
using PersonDirectory.Application.Data;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Infrastructure;

public sealed class ApplicationDbContext(
    DbContextOptions options) : DbContext(options), IUnitOfWork, IApplicationDbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
