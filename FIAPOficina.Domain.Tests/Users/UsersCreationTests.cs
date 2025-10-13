using FIAPOficina.Domain.Users.Entities;

namespace FIAPOficina.Domain.Tests.Users
{
    public class UsersCreationTests
    {
        [Fact]
        public void Should_Create_Valid_User_With_Valid_Name_Username_Id()
        {
            Guid id = Guid.NewGuid();
            User user = new User("Name", "username", id);
            Assert.NotNull(user);
            Assert.Equal("Name", user.Name);
            Assert.Equal("username", user.UserName);
            Assert.Equal(id, user.Id);
        }

        [Fact]
        public void Should_Create_Valid_User_With_Valid_User_Id()
        {
            Guid baseUserId = Guid.NewGuid();
            User baseUser = new User("Name", "username", baseUserId);

            Guid id = Guid.NewGuid();
            User user = new(baseUser, id);

            Assert.NotNull(user);
            Assert.Equal(baseUser.Name, user.Name);
            Assert.Equal(baseUser.UserName, user.UserName);
            Assert.Equal(id, user.Id);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_Invalid_Name()
        {
            var ex = Assert.Throws<ArgumentException>(() => new User(string.Empty, "username"));
            Assert.Equal(nameof(User.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_Invalid_Username()
        {
            var ex = Assert.Throws<ArgumentException>(() => new User("Nome", string.Empty));
            Assert.Equal(nameof(User.UserName), ex.ParamName);
        }
    }
}