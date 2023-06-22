using Moq;
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
using TestNinja.Mocking;

namespace TestNinjaUnitTest.Mocking
{
 
    public class OrderServiceTests
    {
   
        public void PlaceOrder_WhenCalled_StoreTheOrder() 
        {
            var storage = new Mock<IStorage>();
            var service=new OrderService(storage.Object);
            var order =new Order();
            service.PlaceOrder(new Order());
            storage.Verify(s=>s.Store(order));

        }
    }
}
