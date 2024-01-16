using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using System.Data;

public class GenericRepositry<T> : IRepository<T> where T : class
{
    private readonly string _connectionString;

    public GenericRepositry(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public List<T> GetAll(string sql)
    {
        var dbManager = new DbManager(_connectionString);
        return dbManager.GetList<T>(sql);
    }
    public T GetSingle(int id, string sql)
    {
        var dbManager = new DbManager(_connectionString);
        return dbManager.GetSingle<T>(sql, new { Id = id });
    }
    public Result Add(T entity, string sql, ref int newId, DbManager dbManager)
    {
        var id = dbManager.ExecuteScalar<int>(sql, entity);
        if (id != null)
        {
            newId = id;
            return Result.Success;
        }
        else
        {
            return Result.Fail;
        }
    }
  
    public Result Add(string sql, DbManager dbManager)
    {

        var newId = dbManager.ExecuteScalar<int>(sql);
        if (newId != null)
        {

            return Result.Success;
        }
        else
        {
            return Result.Fail;
        }

    }

    public Result Update(T entity, string sql)
    {
        var dbManager = new DbManager(_connectionString);
        dbManager.Execute(sql, entity);
        return Result.Success;
    }

    public Result Delete(int id, string sql)
    {
        var dbManager = new DbManager(_connectionString);
        dbManager.Execute(sql, new { Id = id });
        return Result.Success;
    }

 




    public List<Item> GetOperationDetailsByOperationId(int operationId, string sql)
    {
        var dbManager = new DbManager(_connectionString);
        return dbManager.GetList<Item>(sql, new { OperationId = operationId });
    }
    public List<T> GetAllByUser(string sql)
    {
        var dbManager = new DbManager(_connectionString);
        return dbManager.GetList<T>(sql);
    }

    public T GetUsesr(string email, string password, string sql)
    {
        var dbManager = new DbManager(_connectionString);
        return dbManager.GetSingle<T>(sql, new { Email = email, Password = password });
    }



  
}
