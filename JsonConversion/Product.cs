namespace JsonConversion
{
    public class ProductV3  
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }

    public class ProductV2
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
