using Maps.src.Maps.Core.Models;

namespace Maps.src.Maps.Core.Interfaces
{
    public interface IUserRepository: IBasicRepo<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAdminsAsync();
    }
}
