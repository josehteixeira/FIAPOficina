namespace FIAPOficina.Domain.Users.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string UserName { get; private set; }

        public User(string name, string userName, Guid? id = null)
        {
            CheckRequiredFields(name, userName);

            Id = id.HasValue ? id.Value : Guid.NewGuid();
            Name = name;
            UserName = userName;
        }

        public User(User user, Guid id)
        {
            CheckRequiredFields(user.Name, user.UserName);

            Id = id;
            Name = user.Name;
            UserName = user.UserName;
        }

        private void CheckRequiredFields(string name, string userName)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The user must have a name.", nameof(Name));
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("The user must have a user name.", nameof(UserName));
        }
    }
}