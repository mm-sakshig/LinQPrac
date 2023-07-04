using LinQ_Prac.Data;
using LinQ_Prac.Models;
using Microsoft.Azure.Documents;

namespace LinQ_Prac.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _dataContext;

        public UserRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteUser(int userId)
        {
            UserModel user = _dataContext.User.Where(u => u.Id == userId).SingleOrDefault();
            _dataContext.User.Remove(user);
            _dataContext.SaveChanges();
        }

        public UserModel GetUserById(int userId)
        {
            var query = from u in _dataContext.User
                        where u.Id == userId
                        select u;
            var user = query.FirstOrDefault();
            var model = new UserModel()
            {
                Id = userId,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age
            };
            return model;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            IList<UserModel> userList = new List<UserModel>();
            var query = from user in _dataContext.User
                        select user;
            var users = query.ToList();
            foreach (var userData in users)
            {
                userList.Add(new UserModel()
                {
                    Id = userData.Id,
                    Name = userData.Name,
                    Email = userData.Email,
                    Age = userData.Age
                });
            }
            return userList;
        }

        public void InsertUser(UserModel user)
        {
            var userData = new UserModel()
            {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age
            };
            _dataContext.User.Add(userData);
            _dataContext.SaveChanges();
        }

        public void UpdateUser(UserModel user)
        {
            UserModel userData = _dataContext.User.Where(u => u.Id == user.Id).SingleOrDefault();
            userData.Name = user.Name;
            userData.Email = user.Email;
            userData.Age = user.Age;
            _dataContext.SaveChanges();
        }
    }
}
