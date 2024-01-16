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
        private readonly string _connectionString = "Server=JO-SHAHEEN-PC\\SQLEXPRESS;Database=miniSonic;Integrated Security=SSPI;TrustServerCertificate=True";

        public ItemManager(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }


        public Result Add(Item entity)
        {
            var dbManager = new DbManager(_connectionString);

            string sql = $@"INSERT INTO Item (Name, Price, Discount, Tax, CategoryId)
                      VALUES('{entity.Name}', {entity.Price}, {entity.Discount}, {entity.Tax}, {entity.CategoryId})" + "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var result = _itemRepository.Add(sql,dbManager);

            return result != null ? Result.Success : Result.Fail;
        }

   

        public Result Delete(int id)
        {
            Result result = Result.Success;

            try
            {
                var sql = "DELETE FROM Item WHERE Id = @Id";
                result = _itemRepository.Delete(id, sql);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public List<Item> GetAll()
        {
            var sql = "SELECT * FROM Item";
            return _itemRepository.GetAll(sql);
        }

        public Item GetById(int id)
        {
            var sql = "SELECT * FROM Item WHERE Id = @Id";
            return _itemRepository.GetSingle(id, sql);
        }



        public Result Update(Item entity)
        {
            var sql = "UPDATE Item " +
                      "SET Name = @Name, Price = @Price, Discount = @Discount, Tax = @Tax, CategoryId = @CategoryId " +
                      "WHERE Id = @Id";

            _itemRepository.Update(entity, sql);
            return Result.Success;
        }
    }
}
