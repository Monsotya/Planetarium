using PlanetariumModels;

namespace PlanetariumRepositories
{
    public interface IUsersRepository : IRepository<Users>
    {
        Task<Users> GetByUsernameAsync(string username);

        Task<Users> GetByEmailAsync(string email);

        public List<Users> GetAll();
    }
}