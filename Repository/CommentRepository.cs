using ASP.NET_Web_API_Project.Data;
using ASP.NET_Web_API_Project.Interfaces;
using ASP.NET_Web_API_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Web_API_Project.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var IsExist = await _context.Comments.FirstOrDefaultAsync(X => X.Id == id);
            if (IsExist == null)
            {
                return null;
            }
            _context.Comments.Remove(IsExist);
            await _context.SaveChangesAsync();
            return IsExist;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var Exist = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (Exist == null)
            {
                return null;
            }

            Exist.Title = comment.Title;
            Exist.Content = comment.Content;

            await _context.SaveChangesAsync();
            return Exist;
        }
    }
}
