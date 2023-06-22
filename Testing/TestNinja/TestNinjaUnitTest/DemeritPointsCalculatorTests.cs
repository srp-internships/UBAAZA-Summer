
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
namespace TestNinjaUnitTest
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        [TestCase(-1)]
        public void CalculateDemerPoints_SpeedIsNegative_ThrowArgumentOutOfRangeException(int speed) 
        {
            var calculator= new DemeritPointsCalculator();
            Assert.That(() => calculator.CalculateDemeritPoints(speed),Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
          

        }
        [Test]
        [TestCase(0,0)]
        [TestCase(64,0)]
        [TestCase(65,0)]
        [TestCase(66,0)]
        [TestCase(70,1)]
        [TestCase(75,2)]
        public void CalculateDemerPoints_WhenCalled_ReturnDemeritPoints(int speed,int expectedResult)
        {
            var calculator = new DemeritPointsCalculator();
           var points= calculator.CalculateDemeritPoints(speed);
            Assert.That(points, Is.EqualTo(expectedResult));
        }
      


    }
}
