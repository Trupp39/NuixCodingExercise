using InvestmentPerformanceAPI.Enums;
using InvestmentPerformanceAPI.Models;
using InvestmentPerformanceAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace InvestmentPerformanceAPI.Tests.Services
{
    public class UserInvestmentServiceTests
    {
        private AppDbContext _context;
        private UserInvestmentService _service;
        private Logger<UserInvestmentServiceTests> _logger;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDbContext(options);

            var mockLogger = new Mock<ILogger<UserInvestmentService>>();

            // Seed data
            var user = new User
            {
                Id = 1,
                Name = "Rudy",
                Investments = new List<Investment>
                {
                    new Investment { Id = 1, Name = "QQQ", CostBasisPerShare = 40, CurrentPrice = 70, NumberOfShares = 1, CurrentValue = 70, Term = TermEnum.Short, TotalGains = 30},
                    new Investment { Id = 2, Name = "SPY", CostBasisPerShare = 50, CurrentPrice = 80, NumberOfShares = 2, CurrentValue = 160, Term = TermEnum.Long, TotalGains = 60},
                    new Investment { Id = 3, Name = "SPX", CostBasisPerShare = 60, CurrentPrice = 90, NumberOfShares = 3, CurrentValue = 270, Term = TermEnum.Long, TotalGains = 90},
                }
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            _service = new UserInvestmentService(mockLogger.Object, _context);
        }

        [Test]
        public async Task GetUserInvestments_ReturnsInvestments_WhenUserExists()
        {
            var result = await _service.GetUserInvestments(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].InvestmentName, Is.EqualTo("QQQ"));
            Assert.That(result[1].InvestmentName, Is.EqualTo("SPY"));
            Assert.That(result[1].InvestmentId, Is.EqualTo(2));
        }

        [Test]
        public async Task GetUserInvestments_ReturnsEmpty_WhenUserDoesNotExist()
        {
            var result = await _service.GetUserInvestments(4);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetUserInvestmentDetails_ReturnsInvestmentsDetails_WhenUserAndInvestmentExists()
        {
            var result = await _service.GetUserInvestmentDetails(1,3);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(3));
            Assert.That(result.CurrentValue, Is.EqualTo(270));
            Assert.That(result.CostBasisPerShare, Is.EqualTo(60));
            Assert.That(result.CurrentPrice, Is.EqualTo(90));
            Assert.That(result.Term, Is.EqualTo(TermEnum.Long.ToString()));
            Assert.That(result.TotalGains, Is.EqualTo(90));
        }

        [Test]
        public async Task GetUserInvestmentDetails_ReturnsNull_WhenUserDoesntExist()
        {
            var result = await _service.GetUserInvestmentDetails(5, 3);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetUserInvestmentDetails_ReturnsNull_WhenInvestmentDoesntExist()
        {
            var result = await _service.GetUserInvestmentDetails(1, 5);

            Assert.That(result, Is.Null);
        }
    }
}
