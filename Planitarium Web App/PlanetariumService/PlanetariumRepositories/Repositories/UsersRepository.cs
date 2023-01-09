using Microsoft.EntityFrameworkCore;
using PlanetariumModels;

namespace PlanetariumRepositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(PlanetariumServiceContext repositoryPatternDemoContext) : base(repositoryPatternDemoContext)
        {
        }
        public Task<Users> GetByUsernameAsync(string username)
        {
            return base.GetAll().FirstOrDefaultAsync(u => u.Username.Equals(username));
        }

        public Task<Users> GetByEmailAsync(string email)
        {
            return base.GetAll().FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public List<Users> GetAll()
        {
            return  base.GetAll().ToList<Users>();
        }
    }
}
