using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System.Data;

namespace Mini_Sonic.Service
{
    public class UserService : IGenericRepository<User>
    {
        private readonly UserManager _userManager;

        public UserService(IRepository<User> userRepository)
        {
            _userManager = new UserManager(userRepository);
        }

        public List<User> GetAll()
        {
            return _userManager.GetAll();
        }

        public Result Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }


        public User GetById(int id)
        {
          return  _userManager.GetById(id);
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string email, string password)
        {
            return _userManager.GetUser(email, password);

        }
  
        public Result Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Result Add(Operation entity, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
