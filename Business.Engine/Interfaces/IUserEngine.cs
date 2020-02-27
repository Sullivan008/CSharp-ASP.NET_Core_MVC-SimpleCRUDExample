using System.Threading.Tasks;

namespace Business.Engine.Interfaces
{
    public interface IUserEngine
    {
        Task<bool> Register(string userName, string password);
        
        Task<bool> Login(string userName, string password);

        Task Logout();
    }
}
