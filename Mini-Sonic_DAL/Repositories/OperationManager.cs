using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using System.Data;


namespace Mini_Sonic_DAL.Repositories
{
    public class OperationManager : IGenericRepository<Operation>
    {
        private readonly IRepository<Operation> _operationRepository;




        public OperationManager(IRepository<Operation> operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public Result Add(Operation entity, DbManager dbManager)
        {

          
                var sqlOperation = $@"INSERT INTO Operation (Date, NetTotal, GrossTotal, DiscountTotal, TaxTotal, TypeOperationId)
                                   VALUES(@Date, @netTotal, @GrossTotal, @DiscountTotal, @TaxTotal, @TypeOperationId)
                                  SELECT CAST(SCOPE_IDENTITY() AS INT)";
            int id = -1;
            var operationResult = _operationRepository.Add(entity, sqlOperation, ref id,dbManager); ; ;
            entity.Id = id;

            if (operationResult == Result.Success)
            {

                return Result.Success;
            }
            else
            {
                return Result.Fail;
            }

        }
        public Result AddDetails(List<OperationDetail>entity, DbManager dbManager,int OperationId)

        {
            var operationResult = Result.Success;
            foreach (var item in entity)
            {
                string sqlOperationDetails = $@"INSERT INTO OperationDetail (Quantity, ItemId, OperationId)  
                                        VALUES({item.Quantity},{item.ItemId},{OperationId});";

              operationResult  = _operationRepository.Add(sqlOperationDetails, dbManager);

            }
            if (operationResult == Result.Success)
            {

                return Result.Success;
            }
            else
            {
                return Result.Fail;
            }

        }
     

        public Result Delete(int id)
        {
            Result result = Result.Success;

            try
            {
                var sql = "DELETE FROM Operation WHERE Id = @Id";
                result = _operationRepository.Delete(id, sql);
                return result;
            }
            catch(Exception ex) 
            {
                Console.Error.WriteLine($"An error occurred in the Delete method of CategoryManager: {ex.Message}");
                result = Result.Fail;
                return result;
            }
        }

        public List<Operation> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Operation";
                return _operationRepository.GetAll(sql);
            }
            catch
            {
                return new List<Operation>();
            }
        }

        public Operation GetById(int id)
        {
            try
            {
                var sql = "SELECT * FROM Operation WHERE Id = @Id";
                return _operationRepository.GetSingle(id, sql);
            }
            catch
            {
                return null;
            }
        }

        public Result Update(Operation entity)
        {
            try
            {
                var sql = "UPDATE Operation " +
                          "SET Date = @Date, NetTotal = @NetTotal, GrossTotal = @GrossTotal, " +
                          "DiscountTotal = @DiscountTotal, TaxTotal = @TaxTotal, TypeOperationId = @TypeOperationId " +
                          "WHERE Id = @Id";

                _operationRepository.Update(entity, sql);

                return Result.Success;
            }
            catch
            {
                return Result.Fail;
            }
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            try
            {
                var sql = @"SELECT
                                Quantity,
                                ItemId,
                                OperationId,
                                Item.Name,
                                Item.Price,
                                Item.Discount,
                                Item.Tax
                            FROM
                                OperationDetail
                            JOIN
                                Item ON ItemId = Item.Id
                            WHERE OperationId = @OperationId;";

                return _operationRepository.GetOperationDetailsByOperationId(operationId, sql);
            }
            catch
            {
                // Log or handle the exception as needed
                return new List<Item>();
            }
        }

        public Result Add(Operation entity)
        {
            throw new NotImplementedException();
        }
    }
}
