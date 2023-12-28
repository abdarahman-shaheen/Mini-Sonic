using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using System.Data;
using System.Data.Common;

namespace Mini_Sonic.Service
{
    public class CategoryService
    {
        private readonly string _connectionString;

        public CategoryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

                var sql = "SELECT * FROM Categoty";
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                categories = dbConnection.Query<Category>(sql).ToList();
            }

            return categories;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT * FROM SONIC.Categoty";
            //    connection.Open();
            //    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

            //    DataTable dt = new DataTable();

            //    adapter.Fill(dt);
            //    connection.Close();

            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        var category = new Category
            //        {
            //            id = Convert.ToInt32(dr["Id"]),
            //            name = Convert.ToString(dr["CategoryName"])
            //        };
            //        categories.Add(category);
            //    }
            //}
        }

        public Category AddCategory(Category category)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {
                //var sql = "INSERT INTO SONIC.Categoty (CategoryName) OUTPUT INSERTED.id VALUES (@CategoryName)";
                var sql = "INSERT INTO Categoty (CategoryName)"
                    + "VALUES (@CategoryName)"+"SELECT CAST(SCOPE_IDENTITY() AS INT)";


                var newCategoryId = Dbconnection.QuerySingle<int>(sql, new { CategoryName = category.CategoryName });
                var newCategory = new Category()
                {
                    id = newCategoryId,

                    CategoryName = category.CategoryName
                };
                return newCategory;

            }
        }
        //connection.Open();

        //using (SqlCommand command = new SqlCommand(
        //    "INSERT INTO SONIC.Categoty (CategoryName) OUTPUT INSERTED.id VALUES (@CategoryName)",
        //    connection))
        //{
        //    command.Parameters.Add("@CategoryName", SqlDbType.VarChar, 50).Value = category.CategoryName;

        //    var newCategory = new Category()
        //    {
        //        id = (int)command.ExecuteScalar(),

        //        CategoryName = category.CategoryName
        //    };
        //    connection.Close();





        public Category UpdateCategory(Category category)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {
                //var sql = "INSERT INTO SONIC.Categoty (CategoryName) OUTPUT INSERTED.id VALUES (@CategoryName)";
                var sql = "UPDATE Categoty SET CategoryName = @CategoryName WHERE id = @Id";
                Dbconnection.Execute(sql, new{ CategoryName = category.CategoryName , Id=category.id  });
            }
            return category;
            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();

            //    using (SqlCommand updateCommand = new SqlCommand(
            //        "UPDATE SONIC.Categoty SET CategoryName = @CategoryName WHERE id = @Id",
            //        connection))
            //    {
            //        updateCommand.Parameters.Add("@CategoryName", SqlDbType.VarChar, 50).Value = category.CategoryName;
            //        updateCommand.Parameters.Add("@Id", SqlDbType.Int).Value = category.id;

            //        updateCommand.ExecuteNonQuery();
            //    }

            //    connection.Close();
            //}
        }

        public void DeleteCategory(int id)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {
                //var sql = "INSERT INTO SONIC.Categoty (CategoryName) OUTPUT INSERTED.id VALUES (@CategoryName)";
                var sql = "DELETE FROM Categoty WHERE id = @Id";
                Dbconnection.Execute(sql, new { Id = id });
            }

            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();

            //    using (SqlCommand deleteCommand = new SqlCommand(
            //        "DELETE FROM SONIC.Categoty WHERE id = @Id",
            //        connection))
            //    {
            //        deleteCommand.Parameters.AddWithValue("@Id", id);
            //        int rowsAffected = deleteCommand.ExecuteNonQuery();

            //        if (rowsAffected > 0)
            //        {
            //            connection.Close();
            //            return;
            //        }
            //        else
            //        {
            //            connection.Close();
            //            throw new InvalidOperationException($"Category with id {id} not found.");
            //        }
            //    }
            //}

        }
    }
}

