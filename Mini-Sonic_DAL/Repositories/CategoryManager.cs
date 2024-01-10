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


        public OperationResult Add(Categoty entity)
        {
            var sql = "INSERT INTO Categoty (CategoryName) VALUES (@CategoryName); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var newId = _categoryRepository.Add(entity, sql);

            return newId != null ? OperationResult.Success : OperationResult.Fail;
        }

        public OperationResult Update(Categoty entity)
        {
            var sql = "UPDATE Categoty SET CategoryName = @CategoryName WHERE Id = @Id";
            _categoryRepository.Update(entity, sql);

            return OperationResult.Success;
        }
        public void Delete(int id)
        {
            var sql = "DELETE FROM Categoty WHERE Id = @Id";
            _categoryRepository.Delete(id, sql);
        }

        public List<Categoty> GetAll()
        {
            var sql = "SELECT * FROM Categoty";
            return _categoryRepository.GetAll(sql);
        }

        public Categoty GetById(int id)
        {
            var sql = "SELECT * FROM Categoty WHERE Id = @Id";
            return _categoryRepository.GetById(id, sql);
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            throw new NotImplementedException();
        }

        public OperationResult Add(Operation entity, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
