using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Repositories;
using System.Data;
namespace Mini_Sonic.Service
{
    public class ItemService  :IGenericRepository<Item>
    {

        private readonly ItemManager _itemManager;

        public ItemService(IRepository<Item> itemRepository)
        {
            _itemManager = new ItemManager(itemRepository);
        }

        public Item Add(Item entity)
        {
            return _itemManager.Add(entity);
        }

        public void Delete(int id)
        {
            _itemManager.Delete(id);
        }

        public List<Item> GetAll()
        {
            return _itemManager.GetAll();
        }

        public Item GetById(int id)
        {
            return _itemManager.GetById(id);
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            throw new NotImplementedException();
        }

        public Item Update(Item entity)
        {
            return _itemManager.Update(entity);
        }
    }
}
