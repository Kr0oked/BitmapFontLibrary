using System;
using BitmapFontLibrary.Helper;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Helper
{
    [TestFixture]
    public class IntAdapterTest
    {
        private enum TestEnum
        {
            Value1 = 1,
            Value2 = 2,
            Value3 = 4,
            Value4 = 8,
            Value5 = 16
        }

        private IntAdapter _adapter;

        [SetUp]
        public void Initialize()
        {
            _adapter = new IntAdapter();
        }

        [Test]
        public void TestIntToEnum()
        {
            Assert.AreEqual(_adapter.IntToEnum<TestEnum>(1), TestEnum.Value1);
            Assert.AreEqual(_adapter.IntToEnum<TestEnum>(2), TestEnum.Value2);
            Assert.AreEqual(_adapter.IntToEnum<TestEnum>(4), TestEnum.Value3);
            Assert.AreEqual(_adapter.IntToEnum<TestEnum>(8), TestEnum.Value4);
            Assert.AreEqual(_adapter.IntToEnum<TestEnum>(16), TestEnum.Value5);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "T does not contain key 3")]
        public void TestIntToEnumThrowsArgumentException()
        {
            _adapter.IntToEnum<TestEnum>(3);
        }
    }
}
