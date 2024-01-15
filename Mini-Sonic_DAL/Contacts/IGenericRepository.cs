using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Sonic_DAL.Contacts
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
        Result Add(T entity);
        Result Update(T entity);
        Result Delete(int id);

        public List<Item> GetOperationDetailsByOperationId(int operationId);

    }
    public interface IRepository<T> where T : class
    {
        T GetById(int id, string sql);
        T GetUsesr(string email,string password, string sql);

        List<T> GetAllByUser(string sql);
        List<T> GetAll(string sql);
        T Add(T entity, string sql);
        Result Add(T entity, string sql, ref int id, DbManager dbManager);
        Result Add(string sql, DbManager transaction);
        Result Update(T entity, string sql);
        Result Delete(int id, string sql);
        List<Item> GetOperationDetailsByOperationId(int operationId, string sql);
        //public OperationResult AddOperationWithDetails(OperationDetail entity, string sqlOperation, int operationId);
        //OperationResult AddOperationWithDetails(List<OperationDetail> entitylistDetails, string sqlOperationDetails, int operationId, SqlTransaction transaction);


    }
}
