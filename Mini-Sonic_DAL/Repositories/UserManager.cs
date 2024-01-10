using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Sonic_DAL.Repositories
{
    public class UserManager : IGenericRepository<User>
    {
        private readonly string _connectionString;
        private readonly IRepository<User> _userRepository;

        public UserManager(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            var sql = "SELECT * FROM UserSonic";
            return _userRepository.GetAll(sql);
        }
        public OperationResult Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();


          
        }
        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }

        OperationResult IGenericRepository<User>.Update(User entity)
        {
            throw new NotImplementedException();
        }

        public OperationResult Add(Operation entity, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
