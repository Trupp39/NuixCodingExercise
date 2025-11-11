using InvestmentPerformanceAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceAPI.Services
{
    public class UserInvestmentService : IUserInvestmentService
    {
        private readonly ILogger<UserInvestmentService> _logger;
        private readonly IAppDbContext _context;

        public UserInvestmentService(ILogger<UserInvestmentService> logger, IAppDbContext context) {
            _logger = logger;
            _context = context;
        }

        //gets user investments via userId
        public async Task<List<UserInvestmentDTO>?> GetUserInvestments(int userId)
        {
            _logger.LogInformation("UserInvestmentService - GetUserInvestments");
            var investments = await _context.Users
                .Where(user => user.Id == userId)
                .Select(user => user.Investments)
                .FirstOrDefaultAsync();

            _logger.LogInformation("UserInvestmentService - GetUserInvestments - Database Call Succesfull");

            if (investments == null)
            {
                return null;
            }

            return investments.Select(i => new UserInvestmentDTO
            {
                InvestmentId = i.Id,
                InvestmentName = i.Name

            }).ToList();

        }
        //gets User Investment Details via a UserId and InvestmentId
        public async Task<InvestmentDTO?> GetUserInvestmentDetails(int userId, int investmentId)
        {
            _logger.LogInformation("UserInvestmentService - GetUserInvestmentDetails");
            var investment = await _context.Users
                .Where(user => user.Id == userId)
                .Select(user => user.Investments.Where(i => i.Id == investmentId).FirstOrDefault())
                .FirstOrDefaultAsync();

            _logger.LogInformation("UserInvestmentService - GetUserInvestmentDetails - Database Call Succesfull");
            if (investment == null)
            {
                return null;
            }
            else
            {

                return new InvestmentDTO
                {
                    Id = investment.Id,
                    Name = investment.Name,
                    CostBasisPerShare = investment.CostBasisPerShare,
                    CurrentPrice = investment.CurrentPrice,
                    CurrentValue = investment.CurrentValue,
                    Term = investment.Term.ToString(),
                    TotalGains = investment.TotalGains,
                    NumberOfShares = investment.NumberOfShares,
                };
            }

        }
    }
}
