using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinjaUnitTest
{
    [TestFixture]
    public class HtmlFormatterTests
    {
      
        public void FarmatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();
            var result = formatter.FormatAsBold("abc");
            //Specific
            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);
            //More general
            Assert.That(result,Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result,Does.EndWith("<strong>"));
            Assert.That(result,Does.Contain("abc"));


        }
    }
}
