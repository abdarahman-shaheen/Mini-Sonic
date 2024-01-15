using Dapper;
using Microsoft.Data.SqlClient;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Mini_Sonic.Service
{
    public class OperationService : IGenericRepository<Operation>
    {
        private readonly OperationManager _operationManager;
        private readonly string _connectionString = "Server=JO-SHAHEEN-PC\\SQLEXPRESS;Database=miniSonic;Integrated Security=SSPI;TrustServerCertificate=True";

        public OperationService(IRepository<Operation> operationRepository)
        {
            _operationManager = new OperationManager(operationRepository);
        }

        public List<Operation> GetAll()
        {
            return _operationManager.GetAll();
        }

        public Result Add(Operation entity)
        {
             var dbManager = new DbManager(_connectionString);

            Result operationResult = Result.Success;

                try
                {
                   
                     dbManager.BeginTransaction();
                      operationResult = _operationManager.Add(entity, dbManager);

                    if (operationResult == Result.Success)
                    {
                        operationResult = _operationManager.AddDetails(entity.Items, dbManager, entity.Id); ;
                    }

                    if (operationResult == Result.Success)
                    {

                        dbManager.CommitTransaction();
                    }
                    

                }
                catch
                {
                   
                    dbManager.RollbackTransaction();
                return Result.Fail; 
                }
            finally
            {
                if(operationResult == Result.Fail)
                {
                    dbManager.RollbackTransaction();
                }
                
            }
               

                return Result.Success;
            }
          
        

    //public OperationResult Add(Operation entity, string connectionString)
    //{

    //    using (TransactionScope scope = new TransactionScope())
    //    {
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            using (SqlTransaction transaction = connection.BeginTransaction())
    //            {
    //                try
    //                {
    //                    _operationManager.Add(entity, transaction);

    //                    // If you have other repository calls, you can add them here with the same transaction

    //                    transaction.Commit();
    //                    scope.Complete();
    //                    return OperationResult.Success;
    //                }
    //                catch (Exception ex)
    //                {
    //                    transaction.Rollback();
    //                    return OperationResult.Fail;
    //                }
    //            }
    //        }
    //    }
    //}
    public Result Update(Operation entity)
        {
            try
            {
                _operationManager.Update(entity);
                return Result.Success;
            }
            catch
            {
                // Log or handle the exception as needed
                return Result.Fail;
            }
        }

        public Result Delete(int id)
        {
            return _operationManager.Delete(id);
        }

        public Operation GetById(int id)
        {
            return _operationManager.GetById(id);
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            return _operationManager.GetOperationDetailsByOperationId(operationId);
        }
    }
}
