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

            if (id.HasValue) Id = id.Value;

            Name = name;
            UserName = userName;
        }

        public User(User user, Guid? id = null)
        {
            CheckRequiredFields(user.Name, user.UserName);

            if (id.HasValue) Id = id.Value;

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