
namespace Mini_Sonic.Model
{
    public class Operation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal NetTotal { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public int TypeOperationId { get; set; }

       public List<OperationDetail> Items { get; set; }


    }
}
