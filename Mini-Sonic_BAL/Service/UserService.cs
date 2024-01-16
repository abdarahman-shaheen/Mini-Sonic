using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System;
using System.Collections.Generic;
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
            Result result = Result.Success;
            try
            {
                return _userManager.GetAll();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetAll method of UserService: {ex.Message}");
                result = Result.Fail;
                return new List<User>();
            }
        }

        public Result Add(User entity)
        {
            Result result = Result.Success;
            try
            {
            
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Add method of UserService: {ex.Message}");
                return Result.Fail;
            }
        }

        public Result Delete(int id)
        {
            Result result = Result.Success;
            try
            {
         
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Delete method of UserService: {ex.Message}");
                return Result.Fail;
            }
        }

        public User GetById(int id)
        {
            Result result = Result.Success;
            try
            {
                return _userManager.GetById(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetById method of UserService: {ex.Message}");
                result = Result.Fail;
                return null;
            }
        }
        public User GetUser(string email, string password)
        {
            Result result = Result.Success;
            try
            {
                return _userManager.GetUser(email, password);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetUser method of UserService: {ex.Message}");
                result = Result.Fail;
                return null;
            }
        }

        public Result Update(User entity)
        {
            Result result = Result.Success;
            try
            {
            

                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Update method of UserService: {ex.Message}");
                return Result.Fail;
            }
        }

      
    }
}
