using MySwaggerUIServer.Models;

namespace MySwaggerUIServer.Services
{
    public interface IUsers
    {
        User AddUser(string name, int age);
        bool DeleteUser(int id);
        System.Linq.IOrderedEnumerable<User> GetMoreFourNameUsers();
        User GetUser(int id);
        System.Linq.IOrderedEnumerable<User> GetUsersByAge(int minAge, int maxAge);
        bool PutUser(int id, string newName, int newAge);
    }
}