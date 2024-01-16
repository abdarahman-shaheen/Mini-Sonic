using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System;
using System.Collections.Generic;

namespace Mini_Sonic.Service
{
    public class ItemService : IGenericRepository<Item>
    {
        private readonly ItemManager _itemManager;

        public ItemService(IRepository<Item> itemRepository)
        {
            _itemManager = new ItemManager(itemRepository);
        }

        public Result Add(Item entity)
        {
            Result result = Result.Success;
            try
            {
                result = _itemManager.Add(entity);
                if (result == Result.Success)
                {
                    return Result.Success;
                }
                else
                {
                    return Result.Fail;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Add method of ItemService: {ex.Message}");
                return result;
            }
        }

        public Result Delete(int id)
        {
            Result result = Result.Success;

            try
            {
                result = _itemManager.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Delete method of ItemService: {ex.Message}");
                result = Result.Fail;
                return result;
            }
        }

        public List<Item> GetAll()
        {
            try
            {
                return _itemManager.GetAll();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetAll method of ItemService: {ex.Message}");
                return new List<Item>();
            }
        }

        public Item GetById(int id)
        {
            try
            {
                return _itemManager.GetById(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetById method of ItemService: {ex.Message}");
                return null;
            }
        }


        public Result Update(Item entity)
        {
            Result result = Result.Success;
            try
            {
                result = _itemManager.Update(entity);
                if (result == Result.Success)
                {
                    return Result.Success;
                }
                else
                {
                    return Result.Fail;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Update method of ItemService: {ex.Message}");
                return result;
            }
        }
    }
}
