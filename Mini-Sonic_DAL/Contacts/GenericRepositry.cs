using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Model;
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

        public OperationResult Update(T entity, string sql)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString))
            {
                _connection.Execute(sql, entity);
                return OperationResult.Success;
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

        public List<Item> GetOperationDetailsByOperationId(int operationId, string sql)
        {
            List<Item> operationDetails = new List<Item>();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                operationDetails = dbConnection.Query<Item>(sql, new { OperationId = operationId }).ToList();
            }

            return operationDetails;
        }
        public OperationResult AddOperationWithDetails(List<OperationDetail> entitylistDetails, string sqlOperationDetails, int operationId)
        {
            using (IDbConnection Dbconnection = new SqlConnection(_connectionString))
            {

                foreach (var item in entitylistDetails)
                {
                    Dbconnection.Execute(sqlOperationDetails, new OperationDetail
                    {
                        Quantity = item.Quantity,
                        ItemId = item.ItemId,
                        OperationId = operationId
                    });
                }
            }
            return OperationResult.Success;


        }
        public T Add(T entity, string sql, SqlTransaction transaction)
        {
            // Use the existing connection from the transaction
           
                var newId = transaction.Connection.ExecuteScalar<int>(sql, entity, transaction);
               //_connection.ExecuteScalar<int>("SELECT CAST(SCOPE_IDENTITY() AS INT)", transaction);
                typeof(T).GetProperty("Id").SetValue(entity, newId);
                return entity;
            
        }
   public OperationResult AddOperationWithDetails(List<OperationDetail> entitylistDetails, string sqlOperationDetails, int operationId, SqlTransaction transaction)
{
   

            foreach (var item in entitylistDetails)
            {
                    transaction.Connection.Execute(sqlOperationDetails, new OperationDetail
                {
                    Quantity = item.Quantity,
                    ItemId = item.ItemId,
                    OperationId = operationId
                }, transaction);
            }
        

        return OperationResult.Success;
    }
    }

}

