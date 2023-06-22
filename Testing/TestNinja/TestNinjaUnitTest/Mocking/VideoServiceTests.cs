using Moq;
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
using TestNinja.Mocking;

namespace TestNinjaUnitTest.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        [Test]
        public void ReadVideoTitle_EmtyFile_ReturnError()
        {
            var fileReder = new Mock<IFileReader>();
            fileReder.Setup(fr => fr.Read("video.txt")).Returns("");

            var service =new VideoService();
          //  var result = service.ReadVideoTitle(new FakeFileReader());
           // Assert.That(result, Does.Contain("error").IgnoreCase);

        }

    }
}
