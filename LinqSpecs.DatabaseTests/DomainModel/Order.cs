namespace LinqSpecs.DatabaseTests
{
    public class Order
    {
        public Order(string product, int price)
        {
            Product = product;
            Price = price;
        }

        public int Id { get; private set; }

        public virtual Customer? Customer { get; set; }

        public string Product { get; set; }

        public int Price { get; set; }
    }
}
