using ASP.NET_Web_API_Project.Models;

namespace ASP.NET_Web_API_Project.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id,Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
}
