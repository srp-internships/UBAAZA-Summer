using Moq;
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
using TestNinja.Mocking;

namespace TestNinjaUnitTest.Mocking
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
        var product=new Product { ListPrice=100};
        var result = product.GetPrice(new Customer { IsGold = true });
        Assert.That(result, Is.EqualTo(70));
        
        }
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount2()
        {
            var customer=new Mock<ICustemer>();
            customer.Setup(c=>c.IsGold).Returns(true);

            var product = new Product { ListPrice = 100 };
            var result = product.GetPrice(customer.Object);
            Assert.That(result, Is.EqualTo(70));

        }
    }
}
