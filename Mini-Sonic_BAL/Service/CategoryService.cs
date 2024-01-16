using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System;
using System.Collections.Generic;

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
            Result result = Result.Success;
            try
            {
                result = _categoryManager.Add(entity);
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Add method of CategoryService: {ex.Message}");
                return result;
            }
        }

        public Result Update(Categoty entity)
        {
            Result result = Result.Success;
            try
            {
                result = _categoryManager.Update(entity);
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Update method of CategoryService: {ex.Message}");
                return result;
            }
        }

        public Result Delete(int id)
        {
            Result result = Result.Success;

            try
            {
                result = _categoryManager.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Delete method of CategoryService: {ex.Message}");
                result = Result.Fail;
                return result;
            }
        }

        public List<Categoty> GetAll()
        {
            try
            {
                return _categoryManager.GetAll();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetAll method of CategoryService: {ex.Message}");
                return new List<Categoty>();
            }
        }

        public Categoty GetById(int id)
        {
            try
            {
                return _categoryManager.GetById(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetById method of CategoryService: {ex.Message}");
                return null;
            }
        }

        public List<Categoty> GetUserCategory(int userId)
        {
            try
            {
                return _categoryManager.GetAllByUser(userId);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetUserCategory method of CategoryService: {ex.Message}");
                return new List<Categoty>();
            }
        }

    


    }
}
