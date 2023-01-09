using Microsoft.AspNetCore.Http;
using PlanetariumModels;
using PlanetariumRepositories;
using System.Security.Claims;

namespace PlanetariumServices
{
    public class UsersService : IUsersService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUsersRepository userRepository;

        public UsersService(IHttpContextAccessor httpContextAccessor, IUsersRepository userRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (httpContextAccessor.HttpContext != null)
            {
                result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public Users GetByUsername(string username) => userRepository.GetByUsernameAsync(username).Result;

        public Users GetByEmail(string email) => userRepository.GetByEmailAsync(email).Result;

        public List<Users> GetAll() => userRepository.GetAll().ToList<Users>();

        public async Task<Users> Add(Users user) => await userRepository.AddAsync(user);

    }
}
