using ASP.NET_Web_API_Project.DTOs.Stock;
using ASP.NET_Web_API_Project.Models;

namespace ASP.NET_Web_API_Project.Mappers
{
    public static class StockMappers
    {
        public static StockDto TostockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                comments = stockModel.Comments.Select(x => x.ToCommentDto()).ToList(),
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequest stockDto)
        {
            return new Stock()
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }
    }
}
