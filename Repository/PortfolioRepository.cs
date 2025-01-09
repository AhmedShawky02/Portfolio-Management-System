using ASP.NET_Web_API_Project.Data;
using ASP.NET_Web_API_Project.Interfaces;
using ASP.NET_Web_API_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ASP.NET_Web_API_Project.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolioModel == null)
            {
                return null;
            }
            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfoliol(AppUser user)
        {
            return await _context.Portfolios.Where(x => x.AppUserId == user.Id)
                .Select( X => new Stock
                { 
                    Id = X.Stock.Id,
                    Symbol = X.Stock.Symbol,
                    CompanyName = X.Stock.CompanyName,
                    Purchase = X.Stock.Purchase,
                    LastDiv = X.Stock.LastDiv,
                    Industry = X.Stock.Industry,
                    MarketCap = X.Stock.MarketCap,
                }).ToListAsync();

        }
    }
}
