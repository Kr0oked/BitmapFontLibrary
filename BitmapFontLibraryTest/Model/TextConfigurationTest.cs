using BitmapFontLibrary.Model;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Model
{
    [TestFixture]
    public class TextConfigurationTest
    {
        private TextConfiguration _configuration;

        [SetUp]
        public void Initialize()
        {
            _configuration = new TextConfiguration();
        }

        [Test]
        public void TestInitialValues()
        {
            Assert.AreEqual(_configuration.SizeInEms, 1.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 100);
            Assert.AreEqual(_configuration.SizeInPixels, 16);
            Assert.AreEqual(_configuration.SizeInPoints, 12);
        }

        [Test]
        public void TestSizeConversionFromEms()
        {
            _configuration.SizeInEms = 0.5f;
            Assert.AreEqual(_configuration.SizeInEms, 0.5f);
            Assert.AreEqual(_configuration.SizeInPercent, 50);
            Assert.AreEqual(_configuration.SizeInPixels, 8);
            Assert.AreEqual(_configuration.SizeInPoints, 6);

            _configuration.SizeInEms = 2.0f;
            Assert.AreEqual(_configuration.SizeInEms, 2.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 200);
            Assert.AreEqual(_configuration.SizeInPixels, 32);
            Assert.AreEqual(_configuration.SizeInPoints, 24);

            _configuration.SizeInEms = 3.0f;
            Assert.AreEqual(_configuration.SizeInEms, 3.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 300);
            Assert.AreEqual(_configuration.SizeInPixels, 48);
            Assert.AreEqual(_configuration.SizeInPoints, 36);
        }

        [Test]
        public void TestSizeConversionFromPercent()
        {
            _configuration.SizeInPercent = 50;
            Assert.AreEqual(_configuration.SizeInEms, 0.5f);
            Assert.AreEqual(_configuration.SizeInPercent, 50);
            Assert.AreEqual(_configuration.SizeInPixels, 8);
            Assert.AreEqual(_configuration.SizeInPoints, 6);

            _configuration.SizeInPercent = 200;
            Assert.AreEqual(_configuration.SizeInEms, 2.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 200);
            Assert.AreEqual(_configuration.SizeInPixels, 32);
            Assert.AreEqual(_configuration.SizeInPoints, 24);

            _configuration.SizeInPercent = 300;
            Assert.AreEqual(_configuration.SizeInEms, 3.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 300);
            Assert.AreEqual(_configuration.SizeInPixels, 48);
            Assert.AreEqual(_configuration.SizeInPoints, 36);
        }

        [Test]
        public void TestSizeConversionFromPixels()
        {
            _configuration.SizeInPixels = 8;
            Assert.AreEqual(_configuration.SizeInEms, 0.5f);
            Assert.AreEqual(_configuration.SizeInPercent, 50);
            Assert.AreEqual(_configuration.SizeInPixels, 8);
            Assert.AreEqual(_configuration.SizeInPoints, 6);

            _configuration.SizeInPixels = 32;
            Assert.AreEqual(_configuration.SizeInEms, 2.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 200);
            Assert.AreEqual(_configuration.SizeInPixels, 32);
            Assert.AreEqual(_configuration.SizeInPoints, 24);

            _configuration.SizeInPixels = 48;
            Assert.AreEqual(_configuration.SizeInEms, 3.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 300);
            Assert.AreEqual(_configuration.SizeInPixels, 48);
            Assert.AreEqual(_configuration.SizeInPoints, 36);
        }

        [Test]
        public void TestSizeConversionFromPoints()
        {
            _configuration.SizeInPoints = 6;
            Assert.AreEqual(_configuration.SizeInEms, 0.5f);
            Assert.AreEqual(_configuration.SizeInPercent, 50);
            Assert.AreEqual(_configuration.SizeInPixels, 8);
            Assert.AreEqual(_configuration.SizeInPoints, 6);

            _configuration.SizeInPoints = 24;
            Assert.AreEqual(_configuration.SizeInEms, 2.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 200);
            Assert.AreEqual(_configuration.SizeInPixels, 32);
            Assert.AreEqual(_configuration.SizeInPoints, 24);

            _configuration.SizeInPoints = 36;
            Assert.AreEqual(_configuration.SizeInEms, 3.0f);
            Assert.AreEqual(_configuration.SizeInPercent, 300);
            Assert.AreEqual(_configuration.SizeInPixels, 48);
            Assert.AreEqual(_configuration.SizeInPoints, 36);
        }
    }
}