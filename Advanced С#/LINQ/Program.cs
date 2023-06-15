using LINQ;
using System.ComponentModel;

List<Product> products = Product.GetCustomerList();
void Search_by_quantity()
{
    Console.WriteLine("Search by quantity");
    int a=int.Parse(Console.ReadLine());
    var expensiveInStockProducts = from prod in products
                                   where prod.Count > 0 && prod.Count > a
                                   select prod;
    foreach (var product in expensiveInStockProducts)
    {
        Console.WriteLine($"{product.Name}");
    }
}
void get_name_product()
{
    var productNames = from p in products
                       select p.Name;
    Console.WriteLine("Product Names:");
    foreach (var productName in productNames)
    {
        Console.WriteLine(productName);
    }
}
void SortbyNameandCount()
{
    //var sortedWords = from word in products
    //                  orderby word
    //                  select word;
    var res = products.OrderBy(p => p.Name).ThenByDescending(p => p.Count); ;
    foreach (var w in res)
    {
        Console.WriteLine($"{w.Name} {w.Count}");
    }
}
void first_name()
{
    var first = products.First();//Apple
    Console.WriteLine(first.Name);   
}

var first = products.First();//Apple
var firstWithQuery = products.First(s => s.Name == "Banana");//Banana

Product firstWithAnotherQuery = products.First(s => s.Count > 30);//Exception

Product firstWithAnotherQueryDefault = products.FirstOrDefault(s => s.Count > 30);//null

var last = products.Last();//Apple
var lastWithQuery = products.Last(s => s.Name == "Banana");//Banana

Product lastWithAnotherQuery = products.Last(s => s.Count > 30);//Exception

Product lastWithAnotherQueryDefault = products.LastOrDefault(s => s.Count > 30);//null

Product single = products.Single();//Exception
var id = Guid.NewGuid();
Product singleWithQuery = products.Single(s => s.Id == id);//Apple
Product singleOrDefault = products.SingleOrDefault();//Exception
var id2 = Guid.NewGuid();
Product singleWithQuery2 = products.SingleOrDefault(s => s.Id == id2);//null
var result = products.Any(s => s.Name == "Apple");//true
var result2 = products.All(s => s.Name == "Apple");//false

var containsItem = products.Contains(singleWithQuery);

var exists = products.Any(s => s.Name == "Product1");//true

products.Add(new Product() { Name = "Product1" });







