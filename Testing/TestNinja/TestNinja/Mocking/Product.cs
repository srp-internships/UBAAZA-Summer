namespace TestNinja.Mocking
{
    public class Product
    {
        public float ListPrice { get; set; }

        public float GetPrice(ICustemer customer)
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }
    }
    public interface ICustemer
    {
        bool IsGold { get; set; }
    }
    public class Customer:ICustemer
    {
        public bool IsGold { get; set; }
       
    }
}