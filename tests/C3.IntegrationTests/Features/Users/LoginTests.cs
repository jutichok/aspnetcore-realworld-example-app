using System;
using System.Threading.Tasks;
using C3.Domain;
using C3.Features.Users;
using C3.Infrastructure.Security;
using Xunit;

namespace C3.IntegrationTests.Features.Users
{
    public class LoginTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Login()
        {
            var salt = Guid.NewGuid().ToByteArray();
            var person = new Person
            {
                Username = "username",
                Email = "email",
                Hash = await new PasswordHasher().Hash("password", salt),
                Salt = salt
            };
            await InsertAsync(person);

            var command = new Login.Command()
            {
                User = new Login.UserData()
                {
                    Email = "email",
                    Password = "password"
                }
            };

            var user = await SendAsync(command);

            Assert.NotNull(user?.User);
            Assert.Equal(user.User.Email, command.User.Email);
            Assert.Equal("username", user.User.Username);
            Assert.NotNull(user.User.Token);
        }
    }
}