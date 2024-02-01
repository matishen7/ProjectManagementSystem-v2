using NUnit.Framework;
using Moq;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.Contracts.Features.User.Commands;
using ProjectManagementSystem.Application.Contracts.Persistence;
using AutoMapper;
using ProjectManagementSystem.Application.Middleware;
using System;

namespace ProjectManagementSystem.Application.UnitTests.Features.User
{
    [TestFixture]
    public class CreateUserTests
    {
        [Test]
        public async Task Handle_ValidCommand_ShouldCreateUser()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, mapperMock.Object);
            await handler.Handle(command, CancellationToken.None);
            userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Core.Entities.UserEntity>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var command = new CreateUserCommand
            {
                FirstName = "",
                LastName = "",
                Email = ""
            };
            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, mapperMock.Object);
            var exception = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
            
            exception.Errors.ContainsKey("FirstName").Should().BeTrue();
            exception.Errors["FirstName"].Should().Contain("First name is required");

            exception.Errors.ContainsKey("LastName").Should().BeTrue();
            exception.Errors["LastName"].Should().Contain("Last name is required");

            exception.Errors.ContainsKey("Email").Should().BeTrue();
            exception.Errors["Email"].Should().Contain("'Email' must not be empty.");
        }

        [Test]
        public void Handle_ExceptionThrown_ShouldThrowException()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Core.Entities.UserEntity>()))
                .ThrowsAsync(new System.Exception("Some error occurred"));
            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, mapperMock.Object);
            Assert.ThrowsAsync<System.Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
