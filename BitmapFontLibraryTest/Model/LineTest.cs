using BitmapFontLibrary.Model;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Model
{
    [TestFixture]
    public class LineTest
    {
        private Line _line;

        [SetUp]
        public void Initialize()
        {
            _line = new Line();
        }

        [Test]
        public void TestInitialValues()
        {
            Assert.AreEqual(_line.Text, "");
            Assert.AreEqual(_line.X, 0);
        }

        [Test]
        public void TestEqualsAndHashCode()
        {
            var x = new Line();
            var y = new Line();
            Assert.IsTrue(x.Equals(y) && y.Equals(x));
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }
    }
}