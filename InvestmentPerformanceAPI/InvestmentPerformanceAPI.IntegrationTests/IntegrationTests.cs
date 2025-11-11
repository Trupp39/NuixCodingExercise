using InvestmentPerformanceAPI;
using InvestmentPerformanceAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentPerformanceAPI.IntegrationTests
{
    public class UsersControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.Remove(
                            services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<AppDbContext>))
                        );

                        services.AddDbContext<AppDbContext>(options =>
                            options.UseInMemoryDatabase("IntegrationTestsDb"));

                        var sp = services.BuildServiceProvider();
                        using var scope = sp.CreateScope();
                        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        context.Database.EnsureCreated();

                        context.Users.Add(new User
                        {
                            Id = 1,
                            Name = "Rudy",
                            Investments = new List<Investment>
                            {
                                new Investment { Id = 1, Name = "QQQ", CostBasisPerShare = 40, CurrentPrice = 70, NumberOfShares = 1, CurrentValue = 70 },
                                new Investment { Id = 2, Name = "SPY", CostBasisPerShare = 50, CurrentPrice = 80, NumberOfShares = 2, CurrentValue = 160 }
                            }
                        });
                        context.SaveChanges();
                    });
                });

            _client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        [Test]
        public async Task GetUserInvestments_ReturnsOk_WhenUserExists()
        {
            var response = await _client.GetAsync("/user/1/investments");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Assert.That(json.Contains("QQQ"));
            Assert.That(json.Contains("SPY"));
        }

        [Test]
        public async Task GetUserInvestments_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var response = await _client.GetAsync("user/99/investments");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetUserInvestmentDetails_ReturnsOk_WhenUserExists()
        {
            var response = await _client.GetAsync("/user/1/investments/2");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Assert.That(json.Contains("SPY"));
        }

        [Test]
        public async Task GetUserInvestmentDetails_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var response = await _client.GetAsync("user/99/investments/2");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
