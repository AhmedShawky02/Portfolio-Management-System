using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Web_API_Project.DTOs.Comment
{
    public class UpdateCommentRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title Must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content Must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
