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

        public OperationResult Add(Categoty entity)
        {
            var Result = _categoryManager.Add(entity);
            if (Result != null)
            {
                // If operation was successful, return the entity
                return  OperationResult.Success;
            }
            else
            {
                // If operation failed, you might want to handle it accordingly
                return OperationResult.Fail;
            }
        }

    
        public OperationResult Update(Categoty entity)
        {
            var Result = _categoryManager.Update(entity);
            if (Result == OperationResult.Success)
            {
                // If operation was successful, return the entity
                return OperationResult.Success;
            }
            else
            {
                // If operation failed, you might want to handle it accordingly
                // For now, let's return null, but you can modify this based on your needs
                return OperationResult.Fail;
            }
        }
        public void Delete(int id)
        {
            _categoryManager.Delete(id);
        }

        public List<Categoty> GetAll()
        {
            return _categoryManager.GetAll();
        }

        public Categoty GetById(int id)
        {
            return _categoryManager.GetById(id);
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

