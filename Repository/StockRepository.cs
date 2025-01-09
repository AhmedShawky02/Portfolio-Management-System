using ASP.NET_Web_API_Project.Data;
using ASP.NET_Web_API_Project.DTOs.Stock;
using ASP.NET_Web_API_Project.Helpers;
using ASP.NET_Web_API_Project.Interfaces;
using ASP.NET_Web_API_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Web_API_Project.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var StockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (StockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(StockModel);
            await _context.SaveChangesAsync();
            return StockModel;

        }

        public async Task<Stock> CreateAsync(Stock StockModel)
        {
            await _context.Stocks.AddAsync(StockModel);
            await _context.SaveChangesAsync();
            return StockModel;

        }   

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var StockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            
            if (StockModel == null)
            {
                return null;
            }

            StockModel.Symbol = stockDto.Symbol;
            StockModel.CompanyName = stockDto.CompanyName;
            StockModel.Purchase = stockDto.Purchase;
            StockModel.LastDiv = stockDto.LastDiv;
            StockModel.Industry = stockDto.Industry;
            StockModel.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return StockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(x => x.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    if (query.IsDecsending == true)
                    {
                        stocks = stocks.OrderByDescending(x => x.Symbol);
                    }
                    else
                    {
                        stocks = stocks.OrderBy(x => x.Symbol);
                    }
                }
            }

            var SkipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(SkipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }
    }
}
