using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Service.Interface;
using Moq;
using Microsoft.EntityFrameworkCore;
using WebAPI.Service.Implementation;
using WebAPI.Repository.Data;
using WebAPI.Enums;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class MasterDataControllerTests
    {
        private MasterDataController _controller;
        private Mock<IMasterDataService> _masterDataServiceMock;

        [SetUp]
        public void Setup()
        {
            _masterDataServiceMock = new Mock<IMasterDataService>();
            _controller = new MasterDataController(_masterDataServiceMock.Object);
        }

        [Test]
        public void GetRandomMasterData_ReturnsOkResult()
        {
            // Arrange
            var expectedMasterData = new IsAdminMasterData
            {
                Id = 1,
                Type = IsAdminMasterDataType.Dropdown
            };
            _masterDataServiceMock.Setup(m => m.GetRandomMasterData())
                .Returns(expectedMasterData);

            // Act
            var result = _controller.GetRandomMasterData();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedMasterData, okResult.Value);
        }
    }
}
