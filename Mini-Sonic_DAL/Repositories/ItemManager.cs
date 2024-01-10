using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using System.Data;
namespace Mini_Sonic_DAL.Repositories
{
    public class ItemManager : IGenericRepository<Item>
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemManager(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }


        public OperationResult Add(Item entity)
        {
            string sql = @"INSERT INTO Item (Name, Price, Discount, Tax, CategoryId)
                      VALUES(@Name, @Price, @Discount, @Tax, @CategoryId)" + "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var result = _itemRepository.Add(entity, sql);

            return result != null ? OperationResult.Success : OperationResult.Fail;
        }

        public OperationResult Add(Operation entity, string connectionString)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Item WHERE Id = @Id";
            _itemRepository.Delete(id, sql);
        }

        public List<Item> GetAll()
        {
            var sql = "SELECT * FROM Item";
            return _itemRepository.GetAll(sql);
        }

        public Item GetById(int id)
        {
            var sql = "SELECT * FROM Item WHERE Id = @Id";
            return _itemRepository.GetById(id, sql);
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            throw new NotImplementedException();
        }


        public OperationResult Update(Item entity)
        {
            var sql = "UPDATE Item " +
                      "SET Name = @Name, Price = @Price, Discount = @Discount, Tax = @Tax, CategoryId = @CategoryId " +
                      "WHERE Id = @Id";

            _itemRepository.Update(entity, sql);
            return OperationResult.Success;
        }
    }
}
