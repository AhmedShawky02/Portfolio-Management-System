using ASP.NET_Web_API_Project.DTOs.Comment;
using ASP.NET_Web_API_Project.Models;

namespace ASP.NET_Web_API_Project.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto()
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int StockId)
        {
            return new Comment()
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = StockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequest commentDto)
        {
            return new Comment()
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
        }
    }
}
