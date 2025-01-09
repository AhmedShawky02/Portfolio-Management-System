using ASP.NET_Web_API_Project.DTOs.Stock;
using ASP.NET_Web_API_Project.Helpers;
using ASP.NET_Web_API_Project.Models;

namespace ASP.NET_Web_API_Project.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(int id); //FirstOrDefault CAN BE NULL

        Task<Stock?> GetBySymbolAsync(string symbol); //FirstOrDefault CAN BE NULL

        Task<Stock>CreateAsync(Stock StockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        Task<Stock> DeleteAsync(int id);

        Task<bool> StockExists (int id);
    }
}
