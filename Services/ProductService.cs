using ExampleMvcXtl.Models;

namespace ExampleMvcXtl.Services
{
    public class ProductService : List<Product>
    {
        public ProductService()
        {
            this.AddRange(new Product[]
            {
                new Product() {Id=1 , Name = "IphoneX", Price = 900},
                new Product() {Id=2 , Name = "Iphone 11", Price = 1000},
                new Product() {Id=3 , Name = "Iphone 12", Price = 1100},
                new Product() {Id=4 , Name = "Iphone 13", Price = 10500},
            });
        }
    }
}
