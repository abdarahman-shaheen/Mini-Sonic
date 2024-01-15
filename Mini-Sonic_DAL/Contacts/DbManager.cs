using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Sonic_DAL.Contacts
{
    public class DbManager
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DbManager(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        public void Execute(string sql, object parameters = null)
        {
            OpenConnection();

            if (_transaction != null)
            {
                _connection.Execute(sql, parameters, _transaction);
            }
            else
            {
                _connection.Execute(sql, parameters);
            }
        }

        public List<T> GetList<T>(string sql, object parameters = null)
        {
            OpenConnection();

            if (_transaction != null)
            {
                return _connection.Query<T>(sql, parameters, _transaction).ToList();
            }
            else
            {
                return _connection.Query<T>(sql, parameters).ToList();
            }
        }

        public T GetSingle<T>(string sql, object parameters = null)
        {
            OpenConnection();

            if (_transaction != null)
            {
                return _connection.QueryFirstOrDefault<T>(sql, parameters, _transaction);
            }
            else
            {
                return _connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        public T ExecuteScalar<T>(string sql, object parameters = null)
        {
            OpenConnection();

            if (_transaction != null)
            {
                return _connection.ExecuteScalar<T>(sql, parameters, _transaction);
            }
            else
            {
                return _connection.ExecuteScalar<T>(sql, parameters);
            }
        }

        public void BeginTransaction()
        {
            OpenConnection();
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }
    }
}
