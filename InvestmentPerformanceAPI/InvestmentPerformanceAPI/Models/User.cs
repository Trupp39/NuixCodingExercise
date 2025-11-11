using InvestmentPerformanceAPI.Enums;

namespace InvestmentPerformanceAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Investment> Investments { get; set; } = new List<Investment>();
    }
}
