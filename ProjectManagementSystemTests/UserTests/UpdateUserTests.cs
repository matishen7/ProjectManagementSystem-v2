using NUnit.Framework;
using Moq;
using AutoMapper;
using MediatR;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.Contracts.Persistence;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Application.Contracts.Features.User.Commands;
using ProjectManagementSystem.Application.Middleware;

namespace ProjectManagementSystem.Application.UnitTests.Features.User.Commands
{
    [TestFixture]
    public class UpdateUserTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IMapper> _mockMapper;
        private UpdateUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateUserCommandHandler(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldUpdateUser()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var existingUser = new UserEntity
            {
                Id = command.UserId,
                FirstName = "ExistingFirstName",
                LastName = "ExistingLastName",
                Email = "existing.email@example.com"
            };

            var updatedUser = new UserEntity
            {
                Id = command.UserId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email
            };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(command.UserId)).ReturnsAsync(existingUser);
            _mockMapper.Setup(m => m.Map(command, existingUser)).Returns(updatedUser);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            _mockUserRepository.Verify(repo => repo.UpdateAsync(It.IsAny<UserEntity>()), Times.Once);

        }

        [Test]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                // Missing user ID
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            exception.Errors.ContainsKey("UserId").Should().BeTrue();
            exception.Errors["UserId"].Should().Contain("UserId must be greater than 0");
        }

        [Test]
        public void Handle_UserNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(command.UserId)).ReturnsAsync(null as UserEntity);

            // Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

            //exception.ContainsKey("UserId").Should().BeTrue();
            //exception.Errors["UserId"].Should().Contain("UserId must be greater than 0");
        }
    }
}
