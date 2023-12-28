using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using System.Data;

namespace Mini_Sonic.Service
{
    public class ItemService
    {
        private readonly string _connectionString;

        public ItemService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();

            var sql = "SELECT * FROM Item";
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                items = dbConnection.Query<Item>(sql).ToList();
            }

            return items;


        }

        public Item AddItem(Item item)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {
                //var sql = "INSERT INTO SONIC.Categoty (CategoryName) OUTPUT INSERTED.id VALUES (@CategoryName)";
                var sql = "INSERT INTO Item (Name, Price, Discount, Tax, CategoryId)" +
                    "VALUES(@Name, @Price, @Discount, @Tax, @CategoryId)" + "SELECT CAST(SCOPE_IDENTITY() AS INT)";



                var newItemId = Dbconnection.QuerySingle<int>(sql, item);
                var newItem = new Item()
                {
                    Id = newItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Discount = item.Discount,
                    Tax = item.Tax,
                    CategoryId = item.CategoryId,

                };
                return newItem;

            }
        }
        public Item UpdateItem(Item item)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE Item " +
                          "SET Name = @Name, Price = @Price, Discount = @Discount, Tax = @Tax, CategoryId = @CategoryId " +
                          "WHERE Id = @Id";

                Dbconnection.Execute(sql, item);
                return item;
            }
        }

        public void DeleteItem(int itemId)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Item WHERE Id = @ItemId";
                Dbconnection.Execute(sql, new { ItemId = itemId });
            }
        }

    }
}
