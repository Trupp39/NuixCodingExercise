using InvestmentPerformanceAPI.Models;

namespace InvestmentPerformanceAPI.DTOs
{
    public class UserInvestmentDTO
    {
        public required int InvestmentId { get; set; } = default;
        public required string InvestmentName { get; set; }

    }
}
