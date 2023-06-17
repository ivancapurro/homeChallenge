using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Service.Interface;
using Moq;
using WebAPI.Repository.Interface;
using System.Collections.Generic;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _controller;
        private Mock<IUserService> _userServiceMock;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(service => service.UpdateUser(It.IsAny<User>())).Returns(true);
            _userServiceMock.Setup(service => service.GetUserById(It.IsAny<int>())).Returns((int userId) => new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", IsAdmin = false });
            _controller = new UserController(_userServiceMock.Object);
        }

        [Test]
        public void GetUsers_ReturnsOkResult()
        {
            // Arrange
            _userServiceMock.Setup(u => u.GetUsers())
                .Returns(new List<User>());

            // Act
            var result = _controller.GetUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void AddUser_WithValidUser_ReturnsOkResult()
        {
            // Arrange
            var user = new User { Id = 4, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", IsAdmin = false };

            // Mock the UserService
            _userServiceMock.Setup(service => service.AddUser(user)).Returns(true); // Expecting a call to AddUser with the provided user object

            // Act
            var result = _controller.AddUser(user);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void UpdateUser_WithValidUser_ReturnsOkResult()
        {
            // Arrange
            var user = new User { Id = 3, FirstName = "John", LastName = "Sena", Email = "johnwilliamson@gmail.com", IsAdmin = false };

            // Act
            var result = _controller.UpdateUser(user.Id, user);

            // Assert
            _userServiceMock.Verify(u => u.UpdateUser(user), Times.Once); // Verify that the UpdateUser method is called once with the expected user object
            Assert.IsInstanceOf<OkResult>(result);
        }



        [Test]
        public void DeleteUser_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int userId = 4;

            // Mock the UserService
            _userServiceMock.Setup(service => service.DeleteUser(userId)).Returns(new User()); // User found and deleted successfully

            // Act
            var result = _controller.DeleteUser(userId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void DeleteUser_WithValidInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int userId = 77;
            _userServiceMock.Setup(u => u.DeleteUser(userId))
                .Returns((User)null); // User not found

            // Act
            var result = _controller.DeleteUser(userId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GetUserById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            _userServiceMock.Setup(u => u.GetUserById(userId))
                .Returns(new User { Id = userId, FirstName = "John", LastName = "Williamson", Email = "johnwilliamson@gmail.com", IsAdmin = true });

            // Act
            var result = _controller.GetUserById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetUserById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int invalidUserId = 23;
            _userServiceMock.Setup(u => u.GetUserById(invalidUserId))
                .Returns((User)null); // Simulating a null return from the service when the user is not found

            // Act
            var result = _controller.GetUserById(invalidUserId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}

