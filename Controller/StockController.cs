using ASP.NET_Web_API_Project.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Web_API_Project.Mappers;
using ASP.NET_Web_API_Project.DTOs.Stock;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Web_API_Project.Repository;
using ASP.NET_Web_API_Project.Interfaces;
using ASP.NET_Web_API_Project.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Web_API_Project.Controller
{
    [Route("api/Stock")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _StockRep;

        public StockController(IStockRepository stockRep)
        {
            _StockRep = stockRep;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Stocks = await _StockRep.GetAllAsync(query);

            var StoksDto = Stocks.Select(s => s.TostockDto());

            return Ok(StoksDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Stock = await _StockRep.GetByIdAsync(id);
            if (Stock == null)
            {
                return NotFound();
            }
            return Ok(Stock.TostockDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest StockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var StockModel = StockDto.ToStockFromCreateDTO();
            await _StockRep.CreateAsync(StockModel);
            return CreatedAtAction(nameof(GetById), new { id = StockModel.Id }, StockModel.TostockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var StockModel = await _StockRep.UpdateAsync(id,updateDto);

            if (StockModel == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var StockModel = await _StockRep.DeleteAsync(id);
            if (StockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
