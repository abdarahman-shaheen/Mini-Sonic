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
        private readonly string _connectionString;
        private readonly IRepository<Operation> _operationRepository;

        //public OperationManager(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("DefaultConnection");
        //}


        public OperationManager(IRepository<Operation> operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public OperationResult Add(Operation entity)
        {
            try
            {
                var sqlOperation = "INSERT INTO Operation (Date, NetTotal, GrossTotal, DiscountTotal, TaxTotal, TypeOperationId)" +
                                   "VALUES(@Date, @NetTotal, @GrossTotal, @DiscountTotal, @TaxTotal, @TypeOperationId)" +
                                   "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var sqlOperationDetails = "INSERT INTO OperationDetail (Quantity, ItemId, OperationId) " +
                                        "VALUES(@Quantity, @ItemId, @OperationId);";

                var operationId = _operationRepository.Add(entity, sqlOperation);

                _operationRepository.AddOperationWithDetails(entity.Items, sqlOperationDetails, operationId.Id);

                return OperationResult.Success;
            }
            catch
            {
                return OperationResult.Fail;
            }
        }
        public OperationResult Add(Operation entity, SqlTransaction transaction)
        {
           
                var sqlOperation = "INSERT INTO Operation (Date, NetTotal, GrossTotal, DiscountTotal, TaxTotal, TypeOperationId)" +
                                    "VALUES(@Date, @NetTotal, @GrossTotal, @DiscountTotal, @TaxTotal, @TypeOperationId)" +
                                    "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var sqlOperationDetails = "INSERT INTO OperationDetail (Quantity, ItemId, OperationId) " +
                                          "VALUES(@Quantity, @ItemId, @OperationId);";

                var operationId = _operationRepository.Add(entity, sqlOperation, transaction);

                _operationRepository.AddOperationWithDetails(entity.Items, sqlOperationDetails, operationId.Id, transaction);

                return OperationResult.Success;
            
          
            
        }

        public void Delete(int id)
        {
            try
            {
                var sql = "DELETE FROM Operation WHERE Id = @Id";
                _operationRepository.Delete(id, sql);
            }
            catch
            {
                // Log or handle the exception as needed
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
                // Log or handle the exception as needed
                return new List<Operation>();
            }
        }

        public Operation GetById(int id)
        {
            try
            {
                var sql = "SELECT * FROM Operation WHERE Id = @Id";
                return _operationRepository.GetById(id, sql);
            }
            catch
            {
                // Log or handle the exception as needed
                return null;
            }
        }

        public OperationResult Update(Operation entity)
        {
            try
            {
                var sql = "UPDATE Operation " +
                          "SET Date = @Date, NetTotal = @NetTotal, GrossTotal = @GrossTotal, " +
                          "DiscountTotal = @DiscountTotal, TaxTotal = @TaxTotal, TypeOperationId = @TypeOperationId " +
                          "WHERE Id = @Id";

                _operationRepository.Update(entity, sql);

                return OperationResult.Success;
            }
            catch
            {
                return OperationResult.Fail;
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
    }
}
