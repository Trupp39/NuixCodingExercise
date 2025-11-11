using InvestmentPerformanceAPI.DTOs;

namespace InvestmentPerformanceAPI.Services
{
    public interface IUserInvestmentService
    {
        Task<List<UserInvestmentDTO>?> GetUserInvestments(int userId);
        Task<InvestmentDTO?> GetUserInvestmentDetails(int userId, int investmentId);
    }
}
