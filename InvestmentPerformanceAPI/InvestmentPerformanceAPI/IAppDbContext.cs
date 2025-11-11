using InvestmentPerformanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceAPI
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Investment> Investments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
