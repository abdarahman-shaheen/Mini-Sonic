using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Repositories;
using System.Data;
using System.Data.Common;

namespace Mini_Sonic.Service
{
    public class OperationService : IGenericRepository<Operation>
    {
        private readonly OperationManager _operationManager;


        public OperationService(IRepository<Operation> operationRepository)
        {
            _operationManager = new OperationManager(operationRepository);
        }

        public List<Operation> GetAll()
        {
            return _operationManager.GetAll();
        }

        public Operation Add(Operation entity)
        {
            return _operationManager.Add(entity);
        }

        public Operation Update(Operation entity)
        {
            return _operationManager.Update(entity);
        }

        public void Delete(int id)
        {
            _operationManager.Delete(id);
        }

        public Operation GetById(int id)
        {
            return _operationManager.GetById(id);
        }

        //public void DeleteOperationDetail(int operationDetailId)
        //{
        //    _operationManager.DeleteOperationDetail(operationDetailId);
        //}

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            return _operationManager.GetOperationDetailsByOperationId(operationId);
        }
    }
}
