using PlanetariumModels;

namespace PlanetariumServices
{
    public interface IUsersService
    {
        //string GetMyName();

        Users GetByUsername(string username);

        Users GetByEmail(string email);

        public List<Users> GetAll();
        public Task<Users> Add(Users user);
    }
}
