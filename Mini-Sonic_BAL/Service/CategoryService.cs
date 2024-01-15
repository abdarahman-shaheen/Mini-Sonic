using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System.Data;
using System.Data.Common;

namespace Mini_Sonic.Service
{
    public class CategoryService : IGenericRepository<Categoty>
    {
        private readonly CategoryManager _categoryManager;

        public CategoryService(IRepository<Categoty> categoryRepository)
        {
            _categoryManager = new CategoryManager(categoryRepository);
        }

        public Result Add(Categoty entity)
        {
            var Result = _categoryManager.Add(entity);
            if (Result != null)
            {
                // If operation was successful, return the entity
                return  Result.Success;
            }
            else
            {
                // If operation failed, you might want to handle it accordingly
                return Result.Fail;
            }
        }

    
        public Result Update(Categoty entity)
        {
            var Result = _categoryManager.Update(entity);
            if (Result == Result.Success)
            {
                // If operation was successful, return the entity
                return Result.Success;
            }
            else
            {
                // If operation failed, you might want to handle it accordingly
                // For now, let's return null, but you can modify this based on your needs
                return Result.Fail;
            }
        }
        public Result Delete(int id)
        {
          return  _categoryManager.Delete(id);
        }

        public List<Categoty> GetAll()
        {
            return _categoryManager.GetAll();
        }

        public Categoty GetById(int id)
        {
            return _categoryManager.GetById(id);
        }
        public List<Categoty> GetUserCategory(int userId)
        {
            return _categoryManager.GetAllByUser(userId);
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

