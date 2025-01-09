using ASP.NET_Web_API_Project.DTOs.Comment;
using ASP.NET_Web_API_Project.Interfaces;
using ASP.NET_Web_API_Project.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Web_API_Project.Controller
{
    [Route("api/Comment")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _CommentRepo;
        private readonly IStockRepository _StockRep;

        public CommentController(ICommentRepository CommentRepo, IStockRepository stockRep)
        {
            _CommentRepo = CommentRepo;
            _StockRep = stockRep;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var CommentModel = await _CommentRepo.GetAllAsync();

            var CommentDto = CommentModel.Select(x => x.ToCommentDto());

            return Ok(CommentDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CommentModel = await _CommentRepo.GetByIdAsync(id);

            if (CommentModel == null)
            {
                return NotFound();
            }
            return Ok(CommentModel.ToCommentDto());
        }

        [HttpPost]
        [Route("{StockId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromRoute] int StockId, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _StockRep.StockExists(StockId))
            {
                return BadRequest("Stock does not exist");
            }

            var CommentModel = commentDto.ToCommentFromCreate(StockId);
            await _CommentRepo.CreateAsync(CommentModel);

            return CreatedAtAction(nameof(GetById), new { id = CommentModel.Id }, CommentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateCommentRequest updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Comment = await _CommentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());
            if (Comment == null)
            {
                return NotFound("Comment Not Found");
            }
            return Ok(Comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CommentModel = await _CommentRepo.DeleteAsync(id);

            if(CommentModel == null)
            {
                return NotFound("Comment Not Found");
            }
            return Ok(CommentModel);
        }
        
    }
}
