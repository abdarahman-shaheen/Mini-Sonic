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
    public class CategoryManager : IGenericRepository<Categoty>
    {
        private readonly IRepository<Categoty> _categoryRepository;

        public CategoryManager(IRepository<Categoty> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public Result Add(Categoty entity)
        {
            var sql = "INSERT INTO Categoty (userId,CategoryName) VALUES (@userId,@CategoryName); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var newId = _categoryRepository.Add(entity, sql);

            return newId != null ? Result.Success : Result.Fail;
        }

        public Result Update(Categoty entity)
        {
            var sql = "UPDATE Categoty SET CategoryName = @CategoryName WHERE Id = @Id";
            _categoryRepository.Update(entity, sql);

            return Result.Success;
        }
        public Result Delete(int id)
        {
            var sql = "DELETE FROM Categoty WHERE Id = @Id";
           return _categoryRepository.Delete(id, sql);
        }

        public List<Categoty> GetAll()
        {
            var sql = "SELECT * FROM Categoty ";
            return _categoryRepository.GetAll(sql);
        }

        public Categoty GetById(int id)
        {
            var sql = "SELECT * FROM Categoty WHERE userID = @Id";
            return _categoryRepository.GetById(id, sql);
        }
        public List<Categoty> GetAllByUser(int userId)
        {
            var sql = $"SELECT * FROM Categoty WHERE userId = {userId}";
            return _categoryRepository.GetAllByUser(sql);
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            throw new NotImplementedException();
        }

        public Result Add(Operation entity, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
