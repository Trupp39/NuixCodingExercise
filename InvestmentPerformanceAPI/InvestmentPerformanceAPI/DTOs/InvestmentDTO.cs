using InvestmentPerformanceAPI.Enums;
using InvestmentPerformanceAPI.Models;

namespace InvestmentPerformanceAPI.DTOs
{
    public class InvestmentDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal CurrentPrice { get; set; }
        public string? Term { get; set; }
        public decimal TotalGains { get; set; }
        public decimal NumberOfShares { get; set; }

    }
}
