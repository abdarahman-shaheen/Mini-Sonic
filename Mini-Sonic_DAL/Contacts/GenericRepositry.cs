using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mini_Sonic_DAL.Contacts
{
    public class GenericRepositry<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;

        public GenericRepositry(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public T Add(T entity, string sql)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString))
            {
                var newId = _connection.ExecuteScalar<int>(sql, entity);

                typeof(T).GetProperty("Id").SetValue(entity, newId);

                return entity;
            }
        }

        public void Delete(int id, string sql)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString))
            {
                _connection.Execute(sql, new { Id = id });
            }
        }

        public List<T> GetAll(string sql)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString))
            {
                return _connection.Query<T>(sql).ToList();
            }
        }

        public T GetById(int id, string sql)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString))
            {
                return _connection.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        public T Update(T entity, string sql)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString))
            {
                _connection.Execute(sql, entity);
                return GetById((int)typeof(T).GetProperty("Id").GetValue(entity), $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id");
            }
        }
        public List<Item> GetOperationDetailsByOperationId(int operationId, string sql)
        {
            List<Item> operationDetails = new List<Item>();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                operationDetails = dbConnection.Query<Item>(sql, new { OperationId = operationId }).ToList();
            }

            return operationDetails;
        }
        public Operation AddOperationWithDetails(Operation entity)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {
                Dbconnection.Open();
                using (var transaction = Dbconnection.BeginTransaction())
                {
                    try
                    {
                        var sqlOperation = "INSERT INTO Operation (Date, NetTotal, GrossTotal, DiscountTotal, TaxTotal, TypeOperationId)" +
                            "VALUES(@Date, @NetTotal, @GrossTotal, @DiscountTotal, @TaxTotal, @TypeOperationId)" +
                            "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                        var newOperationId = Dbconnection.QuerySingle<int>(sqlOperation, entity, transaction);
                        var newOperation = new Operation()
                        {
                            Id = newOperationId,
                            Date = entity.Date,
                            NetTotal = entity.NetTotal,
                            GrossTotal = entity.GrossTotal,
                            DiscountTotal = entity.DiscountTotal,
                            TaxTotal = entity.TaxTotal,
                            TypeOperationId = entity.TypeOperationId,
                            Items = entity.Items,
                        };

                        foreach (var item in entity.Items)
                        {
                            var sqlOperationDetails = "INSERT INTO OperationDetail (Quantity, ItemId, OperationId) " +
                                "VALUES(@Quantity, @ItemId, @OperationId);";

                            Dbconnection.Execute(sqlOperationDetails, new OperationDetail
                            {
                                Quantity = item.Quantity,
                                ItemId = item.ItemId,
                                OperationId = newOperation.Id
                            }, transaction);
                        }

                        transaction.Commit();

                        return newOperation;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Log or handle the exception as needed
                        throw;
                    }
                }
            }
        }
    }
}