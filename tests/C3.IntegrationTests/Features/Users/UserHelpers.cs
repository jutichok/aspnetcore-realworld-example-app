using System.Threading.Tasks;
using C3.Features.Users;

namespace C3.IntegrationTests.Features.Users
{
    public static class UserHelpers
    {
        public static readonly string DefaultUserName = "username";

        /// <summary>
        /// creates a default user to be used in different tests
        /// </summary>
        /// <param name="fixture"></param>
        /// <returns></returns>
        public static async Task<User> CreateDefaultUser(SliceFixture fixture)
        {
            var command = new Create.Command()
            {
                User = new Create.UserData()
                {
                    Email = "email",
                    Password = "password",
                    Username = DefaultUserName
                }
            };

            var commandResult = await fixture.SendAsync(command);
            return commandResult.User;
        }
    }
}
