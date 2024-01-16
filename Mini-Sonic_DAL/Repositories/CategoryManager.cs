using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mini_Sonic_DAL.Repositories
{
    public class CategoryManager : IGenericRepository<Categoty>
    {
        private readonly IRepository<Categoty> _categoryRepository;
        private readonly string _connectionString = "Server=JO-SHAHEEN-PC\\SQLEXPRESS;Database=miniSonic;Integrated Security=SSPI;TrustServerCertificate=True";

        public CategoryManager(IRepository<Categoty> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Result Add(Categoty entity)
        {
            Result result = Result.Success;
            try
            {
                var dbManager = new DbManager(_connectionString);
                var sql = $"INSERT INTO Categoty (userId, CategoryName) VALUES ({entity.userId}, '{entity.CategoryName}'); SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var newId = _categoryRepository.Add(sql, dbManager);

                result = newId != null ? Result.Success : Result.Fail;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Add method of CategoryManager: {ex.Message}");
                result = Result.Fail;
            }

            return result;
        }

        public Result Update(Categoty entity)
        {
            Result result = Result.Success;
            try
            {
                var sql = "UPDATE Categoty SET CategoryName = @CategoryName WHERE Id = @Id";
                _categoryRepository.Update(entity, sql);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Update method of CategoryManager: {ex.Message}");
                result = Result.Fail;
            }

            return result;
        }

        public Result Delete(int id)
        {
            Result result = Result.Success;

            try
            {
                var sql = "DELETE FROM Categoty WHERE Id = @Id";
                result = _categoryRepository.Delete(id, sql);
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Delete method of CategoryManager: {ex.Message}");
                result = Result.Fail;
                return result;
            }
        }

        public List<Categoty> GetAll()
        {
            Result result = Result.Success;
            try
            {
                var sql = "SELECT * FROM Categoty ";
                return _categoryRepository.GetAll(sql);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetAll method of CategoryManager: {ex.Message}");
                result = Result.Fail;
                return new List<Categoty>();
            }
        }

        public Categoty GetById(int id)
        {
            Result result = Result.Success;
            try
            {
                var sql = "SELECT * FROM Categoty WHERE userID = @Id";
                return _categoryRepository.GetSingle(id, sql);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetById method of CategoryManager: {ex.Message}");
                result = Result.Fail;
                return null;
            }
        }

        public List<Categoty> GetAllByUser(int userId)
        {
            Result result = Result.Success;
            try
            {
                var sql = $"SELECT * FROM Categoty WHERE userId = {userId}";
                return _categoryRepository.GetAllByUser(sql);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetAllByUser method of CategoryManager: {ex.Message}");
                result = Result.Fail;
                return new List<Categoty>();
            }
        }

    }
}
