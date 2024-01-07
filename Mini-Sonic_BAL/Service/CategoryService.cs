using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
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

        public Categoty Add(Categoty entity)
        {
            return _categoryManager.Add(entity);
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

        public Categoty Update(Categoty entity)
        {
            return _categoryManager.Update(entity);
        }
    }
}

