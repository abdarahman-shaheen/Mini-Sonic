
namespace Mini_Sonic.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}

