using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;

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

      

        public Operation Add(Operation entity)
        {
        return   _operationRepository.AddOperationWithDetails(entity);
            
        }
        public void Delete(int id)
        {
            var sql = "DELETE FROM Operation WHERE Id = @Id";
            _operationRepository.Delete(id, sql);
        }

        public List<Operation> GetAll()
        {
            var sql = "SELECT * FROM Operation";
            return _operationRepository.GetAll(sql);
        }

        public Operation GetById(int id)
        {
            var sql = "SELECT * FROM Operation WHERE Id = @Id";
            return _operationRepository.GetById(id, sql);
        }

        public Operation Update(Operation entity)
        {
            var sql = "UPDATE Operation " +
                      "SET Date = @Date, NetTotal = @NetTotal, GrossTotal = @GrossTotal, " +
                      "DiscountTotal = @DiscountTotal, TaxTotal = @TaxTotal, TypeOperationId = @TypeOperationId " +
                      "WHERE Id = @Id";

            _operationRepository.Update(entity, sql);
            return entity;
        }

        //public void DeleteOperationDetail(int operationDetailId)
        //{
        //    var sql = "DELETE FROM OperationDetail WHERE OperationId = @OperationDetailId";
        //    _operationRepository.Delete(operationDetailId, sql);
        //}
        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            var sql = @"SELECT
    Quantity,
    ItemId,
    OperationId,
    Item.Name,
    Item.Price,
	Item.Discount,
	item.Tax
FROM
    OperationDetail
JOIN
    Item ON ItemId = Item.Id
	Where OperationId =@OperationId;";
            
         return  _operationRepository.GetOperationDetailsByOperationId(operationId, sql);

           
        }
    }
}
