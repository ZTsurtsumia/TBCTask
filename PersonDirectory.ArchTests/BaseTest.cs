using PersonDirectory.Api;
using PersonDirectory.Application.Abstractions.Messaging;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Infrastructure;
using System.Reflection;

namespace PersonDirectory.ArchTests
{
    public abstract class BaseTest
    {
        protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;

        protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

        protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;

        protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
    }
}
