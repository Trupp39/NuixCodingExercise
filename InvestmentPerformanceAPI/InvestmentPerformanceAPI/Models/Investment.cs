using InvestmentPerformanceAPI.Enums;

namespace InvestmentPerformanceAPI.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal CurrentPrice { get; set; }
        public TermEnum Term { get; set; }
        public decimal TotalGains { get; set; }
    }
}
