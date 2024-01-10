﻿using Dapper;
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
        //private readonly string _connectionString = "Server=JO-SHAHEEN-PC\\SQLEXPRESS;Database=miniSonic;Integrated Security=SSPI;TrustServerCertificate=True";

        public OperationService(IRepository<Operation> operationRepository)
        {
            _operationManager = new OperationManager(operationRepository);
        }

        public List<Operation> GetAll()
        {
            return _operationManager.GetAll();
        }

        public OperationResult Add(Operation entity)
        {
            try
            {
                _operationManager.Add(entity);
                return OperationResult.Success;
            }
            catch
            {
                // Log or handle the exception as needed
                return OperationResult.Fail;
            }
        }
        public OperationResult Add(Operation entity, string connectionString)
        {
           
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                _operationManager.Add(entity, transaction);

                                // If you have other repository calls, you can add them here with the same transaction

                                transaction.Commit();
                                scope.Complete();
                                return OperationResult.Success;
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return OperationResult.Fail;
                            }
                        }
                    }
                }
            
           
   
            
        }

        public OperationResult Update(Operation entity)
        {
            try
            {
                _operationManager.Update(entity);
                return OperationResult.Success;
            }
            catch
            {
                // Log or handle the exception as needed
                return OperationResult.Fail;
            }
        }

        public void Delete(int id)
        {
            _operationManager.Delete(id);
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