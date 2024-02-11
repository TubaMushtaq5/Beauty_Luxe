using BeautyLuxe.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautyLuxe.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User getUserById(string id)
        {
            
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
