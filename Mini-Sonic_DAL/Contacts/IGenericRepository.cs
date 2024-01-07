using Mini_Sonic.Model;
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
        T Add(T entity);
        T Update(T entity);
        void Delete(int id);

        public List<Item> GetOperationDetailsByOperationId(int operationId);

    }
    public interface IRepository<T> where T : class
    {
        T GetById(int id,string sql);

        List<T> GetAll(string sql);
        T Add(T entity, string sql);
        T Update(T entity, string sql);
        void Delete(int id, string sql);
        List<Item> GetOperationDetailsByOperationId(int operationId,string sql);
        public Operation AddOperationWithDetails(Operation entity);
    }
}
