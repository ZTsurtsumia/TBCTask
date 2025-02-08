﻿using FluentAssertions;
using NetArchTest.Rules;
using PersonDirectory.Application.Abstractions.Messaging;

namespace PersonDirectory.ArchTests.Application
{
    public class ApplicationTests : BaseTest
    {
        [Fact]
        public void CommandHandler_ShouldHave_NameEndingWith_CommandHandler()
        {
            TestResult result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Should()
                .HaveNameEndingWith("CommandHandler")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void CommandHandler_Should_NotBePublic()
        {
            TestResult result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Should()
                .NotBePublic()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void QueryHandler_ShouldHave_NameEndingWith_QueryHandler()
        {
            TestResult result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .HaveNameEndingWith("QueryHandler")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void QueryHandler_Should_NotBePublic()
        {
            TestResult result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .NotBePublic()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
