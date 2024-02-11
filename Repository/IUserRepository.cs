using BeautyLuxe.Areas.Identity.Data;

namespace BeautyLuxe.Repository
{
    public interface IUserRepository
    {
        User getUserById(string id);
    }
}
