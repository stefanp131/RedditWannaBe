using RedditWannaBe.DAL.Entities;
using System.Threading.Tasks;

namespace RedditWannaBe.DAL.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
