using System;
using BitmapFontLibrary.Helper;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Helper
{
    [TestFixture]
    public class StringAdapterTest
    {
        private StringAdapter _adapter;

        [SetUp]
        public void Initialize()
        {
            _adapter = new StringAdapter();
        }

        [Test]
        public void TestCommaSeparatedIntStringToIntArray()
        {
            Assert.AreEqual(_adapter.CommaSeparatedIntStringToIntArray("1,2,3,4"), new[] {1, 2, 3, 4});
            
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void TestCommaSeparatedIntStringToIntArrayWithEmptyStringThrowsFormatException()
        {
            _adapter.CommaSeparatedIntStringToIntArray("");
        }        
        
        [Test]
        [ExpectedException(typeof(FormatException))]
        public void TestCommaSeparatedIntStringToIntArrayWithInvalidValueThrowsFormatException()
        {
            _adapter.CommaSeparatedIntStringToIntArray("1,2,3,a");
        }
    }
}
