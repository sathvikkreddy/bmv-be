using Backend.Models;

namespace Backend.Services
{
    public class UserService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<User> GetAllUsers()
        {
            return _bmvContext.Users.ToList();
        }
        public User GetUserById(int id)
        {
            return _bmvContext.Users.Find(id);
        }
        public bool AddUser(User u)
        {
            try
            {
                _bmvContext.Users.Add(u);
                _bmvContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateUser(int id, User u)
        {
            var uu = _bmvContext.Users.Find(id);
            if (uu != null)
            {
                uu.Email = u.Email;
                uu.Password = u.Password;
                uu.CreatedAt = u.CreatedAt;
                _bmvContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteUser(int id)
        {
            var du = _bmvContext.Users.Find(id);
            if (du != null)
            {
                _bmvContext.Users.Remove(du);
                _bmvContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
