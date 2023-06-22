
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
using TestNinja.Mocking;

namespace TestNinjaUnitTest
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}
