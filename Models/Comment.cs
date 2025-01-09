using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Web_API_Project.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; } // Foreign Key
        public Stock? Stock { get; set; } // Navigation Property

    }
}
