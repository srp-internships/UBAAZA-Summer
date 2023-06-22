
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinjaUnitTest
{
    [TestFixture]
    public class ReservationTest
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation=new Reservation();
           // Act,
           var result=reservation.CanBeCancelledBy(new User { IsAdmin=true});
            // Assert.
            Assert.IsTrue(result);  
        }
        [Test]
        public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnsTrue()
        {
            var user =new User();
            var reservation=new Reservation { MadeBy=user};
            var result=reservation.CanBeCancelledBy(user);
            Assert.IsTrue(result);
        }
        [Test]
        public void CanBeCancelledBy_AnotherUserCancellingReservatino()
        {
            var reservation = new Reservation{MadeBy = new User()};
            var result = reservation.CanBeCancelledBy(new User());
            Assert.IsFalse(result);
        }
    }
}
