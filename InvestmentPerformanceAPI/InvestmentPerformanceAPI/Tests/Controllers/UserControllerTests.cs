using InvestmentPerformanceAPI.Controllers;
using InvestmentPerformanceAPI.DTOs;
using InvestmentPerformanceAPI.Services;
using InvestmentPerformanceAPI.Tests.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace InvestmentPerformanceAPI.Tests.Controllers
{
    public class UsersControllerTests
    {
        private Mock<IUserInvestmentService> _serviceMock;
        private Mock<ILogger<UserController>> _loggerMock;
        private UserController _controller;

        [OneTimeSetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IUserInvestmentService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_loggerMock.Object, _serviceMock.Object);
        }

        [Test]
        public async Task GetUserInvestments_ReturnsOk_WhenInvestmentsExist()
        {
            var investments = new List<UserInvestmentDTO>
            {
                new UserInvestmentDTO { InvestmentId = 1, InvestmentName = "QQQ" }
            };
            _serviceMock.Setup(s => s.GetUserInvestments(1)).ReturnsAsync(investments);

            var result = await _controller.GetUserInvestments(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            Assert.That(investments, Is.EqualTo(okResult.Value));
        }

        [Test]
        public async Task GetUserInvestments_ReturnsNotFound_WhenUserDoesNotExist()
        {
            _serviceMock.Setup(s => s.GetUserInvestments(999)).ReturnsAsync((List<UserInvestmentDTO>?)null);

            var result = await _controller.GetUserInvestments(999);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetUserInvestmentDetails_ReturnsNotFound_WhenUserDoesNotExist()
        {
            _serviceMock.Setup(s => s.GetUserInvestmentDetails(99, 3)).ReturnsAsync((InvestmentDTO?)null);

            var result = await _controller.GetUserInvestments(999);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetUserInvestmentDetails_ReturnsOk_WhenUserInvestmentsExist()
        {
            var investment = new InvestmentDTO
            {
                Id = 4,
                CostBasisPerShare = 12,
                CurrentPrice = 15,
                CurrentValue = 12,
                NumberOfShares = 4,
                Name = "Test",
                Term = "Long",
                TotalGains = 4000,
            };
            _serviceMock.Setup(s => s.GetUserInvestmentDetails(1, 4)).ReturnsAsync(investment);

            var result = await _controller.GetUserInvestments(1, 4);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            Assert.That(investment, Is.EqualTo(okResult.Value));
        }
    }
}
