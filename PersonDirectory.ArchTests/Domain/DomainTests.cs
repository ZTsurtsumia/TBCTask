using FluentAssertions;
using NetArchTest.Rules;
using PersonDirectory.Domain.Abstractions;
using System.Reflection;

namespace PersonDirectory.ArchTests.Domain
{
    public class DomainTests : BaseTest
    {
        [Fact]
        public void Entities_ShouldHave_PrivateParameterlessConstructor()
        {
            IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity))
                .GetTypes();

            var failingTypes = new List<Type>();
            foreach (Type entityType in entityTypes)
            {
                ConstructorInfo[] constructors = entityType.GetConstructors(BindingFlags.NonPublic |
                                                                            BindingFlags.Instance);

                if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
                {
                    failingTypes.Add(entityType);
                }
            }

            failingTypes.Should().BeEmpty();
        }
    }
}
