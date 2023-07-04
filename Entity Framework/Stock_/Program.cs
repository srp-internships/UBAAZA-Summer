using System;
using System.Security.Cryptography;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.IO.MemoryMappedFiles;
using System.Data.Entity;

namespace Stock_
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine(@"Выберите дествия 
1-Дабавления Product;
2-Дабавления Customer;
3-Дабавления Supplier;
4-Прибытия
5-Продажи
6-Отчет
0-Завершения работы 
");
             
                int a = int.Parse(Console.ReadLine());
                if (a == 0)
                {
                    Console.Write("Завершения работы ");
                    break;
                }
                else if (a == 1)
                {
                    Console.Write("Напишите Имя продукта :");
                    string name = Console.ReadLine();
                    Console.Write("Напишите количесто продукта :");
                    int quanti = int.Parse(Console.ReadLine());
                    AddProduct(name, quanti);
                }
                else if (a == 2)
                {
                    Console.Write("Напишите Имя Customer :");
                    string name = Console.ReadLine();
                    Console.Write("Напишите adres  :");
                    string adres = Console.ReadLine();
                    AddCustomer(name, adres);
                }
                else if (a == 3)
                {
                    Console.Write("Напишите Имя Supplier :");
                    string name = Console.ReadLine();
                    Console.Write("Напишите adres  :");
                    string adres = Console.ReadLine();
                    AddSupplier(name, adres);
                }
                else if (a == 4)
                {
                    Console.Write("Напишите ID продукта :");
                    int productId = int.Parse(Console.ReadLine());
                    Console.Write("Напишите количесто продукта :");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Напишите цена на одного товара :");
                    int price = int.Parse(Console.ReadLine());
                    Console.Write("Напишите Id Supplier :");
                    int supplierId = int.Parse(Console.ReadLine());
                    AddArrivals(productId, quantity, price, supplierId);
                }
                else if (a == 5)
                {
                    Console.Write("Напишите ID продукта :");
                    int productId = int.Parse(Console.ReadLine());
                    Console.Write("Напишите количесто продукта :");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Напишите цена на одного товара :");
                    int price = int.Parse(Console.ReadLine());
                    Console.Write("Напишите Id Customer :");
                    int customerId = int.Parse(Console.ReadLine());
                    AddSales(customerId, productId, quantity, price);
                }
                else if (a == 6)
                {
                    Report();
                }
                else
                {
                    Console.WriteLine("Такой дествия не сушествует");
                }
            }
        }
        static void AddCustomer(string name, string addres)
        {
            using (var context = new My_DbContext())
            {
                var customer = new Customer
                {
                    Name = name,
                    Address = addres,
                };
                context.Customers.Add(customer);
                context.SaveChanges();

                Console.WriteLine($"Успешно дабавлено : Id={customer.Id},Name={customer.Name} ,Address={customer.Address} ");
                Console.ReadLine();
            }
        }
        static void AddProduct(string name, int quantity)
        {
            using (var context = new My_DbContext())
            {

                var product = new Product
                {
                    Name = name,
                    Quantity = quantity

                };

                context.Products.Add(product);
                context.SaveChanges();
                Console.WriteLine($" Успешно дабавлено : Id={product.Id},name={product.Name} ,quantity={product.Quantity} ");
                Console.ReadLine();
            }
        }
        static void AddSupplier(string name, string address)
        {
            using (var context = new My_DbContext())
            {

                var supplier = new Supplier
                {
                    Name = name,
                    Address = address,

                };

                context.Suppliers.Add(supplier);
                context.SaveChanges();
                Console.WriteLine($"Успешно дабавлено : Id={supplier.Id},name={supplier.Name} ,Address={supplier.Address} ");
                Console.ReadLine();
            }
        }
        static void AddArrivals(int productId, int quantity, int price, int supplierId)
        {
            using (var context = new My_DbContext())
            {

                var arrivals = new Arrivals
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = price,
                    SupplierId = supplierId

                };
                context.Arrivals.Add(arrivals);
                var s = context.Products.SingleOrDefault(x => x.Id == productId);
                if (s == null)
                {
                    Console.WriteLine("Не найдено продукт");
                }
                else
                {
                    var arr = context.Sales.Single(x => x.Id == productId);
                    var sum_sales = price * quantity;
                    var sum = sum_sales - (arr.Price * arr.Quantity);
                    Console.WriteLine($"Купленно ={quantity}; Cумма= {sum_sales}; цена на один штук={price}");
                    Console.WriteLine($"На складе={s.Quantity};");
                    s.Quantity += quantity;
                    context.SaveChanges();
                }
            }
        }
        static void AddSales(int customerId, int productId, int quantity, int price)
        {
            using (var context = new My_DbContext())
            {
                var sales = new Sales
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Price = price,
                    Quantity = quantity
                };
                context.Sales.Add(sales);

                var s = context.Products.SingleOrDefault(x => x.Id == productId);
                if (s == null)
                {
                    Console.WriteLine("Не найдено продукт");
                }
                else
                {
                    var arr = context.Arrivals.Single(x => x.Id == productId);
                    var sum_arr = arr.Price * arr.Quantity;
                    var sum_sales = price * quantity;
                    var sum = sum_sales - (arr.Price * arr.Quantity);
                    s.Quantity -= quantity;
                    context.SaveChanges();
                    Console.WriteLine($"Продоно ={quantity}; Cумма= {sum_sales}; цена на один штук={price}");
                    Console.WriteLine($"Было Купленно ={arr.Quantity}; Cумма ={sum_arr}; на один штук={arr.Price} ");
                    Console.WriteLine($"Прибыл={sum}; На складе осталость={s.Quantity};");
                }
            }
        }
        static void Report()
        {
            using (var context = new My_DbContext())
            {
                var result_sales = context.Sales.Select(x => x.Quantity * x.Price).Sum();
                Console.WriteLine($"Было продоно сумма={result_sales};");
                var result_arrivals = context.Arrivals.Select(x => x.Quantity * x.Price).Sum();
                Console.WriteLine($"Было куплено на сумма={result_arrivals};");
                var result_sum = result_sales - result_arrivals;
                Console.WriteLine($"Обще прибыл={result_sum};");

            }
            Repostcostomer();

        }

        static void Repostcostomer()
        {
            using (var context = new My_DbContext())
            {
                //var employees = from p in context.Customers
                //                join c in context.Sales on p.Id equals c.ProductId
                //                select new { Name = p.Name, Adress = p.Address, Quantity = c.Quantity, Price = c.Price };
                //foreach (var emp in employees)
                //{
                //    Console.WriteLine($"Name={emp.Name}, Adress{emp.Adress} ,{emp.Quantity} ,{emp.Price}");
                //}

                //var employees = from s in context.Sales
                //                join c in context.Customers on s.CustomerId equals c.Id
                //                group s.Customer in g
                //                select 

                var customers = context.Sales.Include(s => s.Customer)
                    .GroupBy(s => s.Customer)
                    .Select(s => new
                    {
                        Customer = s.Key.Name,
                        Total = s.Sum(x => x.Quantity * x.Price)
                    });
                foreach (var c in customers)
                {
                    Console.WriteLine($"Customer: {c.Customer}, totalSum: {c.Total}");
                }


            }

        }


    }
}
