using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Mini_Sonic.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public int CategoryId { get; set; }
    }
}

