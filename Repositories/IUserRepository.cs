using LinQ_Prac.Models;

namespace LinQ_Prac.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(int userId);
        void InsertUser(UserModel user);
        void DeleteUser(int userId);
        void UpdateUser(UserModel user);
    }
}
