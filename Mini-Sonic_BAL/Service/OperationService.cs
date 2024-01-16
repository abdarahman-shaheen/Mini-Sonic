using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System;
using System.Collections.Generic;

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
            try
            {
                return _operationManager.GetAll();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetAll method of OperationService: {ex.Message}");
                return new List<Operation>();
            }
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
                    operationResult = _operationManager.AddDetails(entity.Items, dbManager, entity.Id);
                }

                if (operationResult == Result.Success)
                {
                    dbManager.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                dbManager.RollbackTransaction();
                Console.Error.WriteLine($"An error occurred in the Add method of OperationService: {ex.Message}");
                return Result.Fail;
            }
            finally
            {
                if (operationResult == Result.Fail)
                {
                    dbManager.RollbackTransaction();
                }
            }

            return Result.Success;
        }

        public Result Update(Operation entity)
        {
            try
            {
                _operationManager.Update(entity);
                return Result.Success;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Update method of OperationService: {ex.Message}");
                return Result.Fail;
            }
        }

        public Result Delete(int id)
        {
            Result result = Result.Success;
            try
            {
                result = _operationManager.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the Delete method of OperationService: {ex.Message}");
                result = Result.Fail;
                return result;
            }
        }

        public Operation GetById(int id)
        {
            try
            {
                return _operationManager.GetById(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetById method of OperationService: {ex.Message}");
                return null;
            }
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            try
            {
                return _operationManager.GetOperationDetailsByOperationId(operationId);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred in the GetOperationDetailsByOperationId method of OperationService: {ex.Message}");
                return new List<Item>();
            }
        }
    }
}
