using BitmapFontLibrary.Model;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Model
{
    [TestFixture]
    public class CharacterTest
    {
        private Character _character;

        [SetUp]
        public void Initialize()
        {
            _character = new Character();
        }

        [Test]
        public void TestInitialValues()
        {
            Assert.AreEqual(_character.X, 0);
            Assert.AreEqual(_character.Y, 0);
            Assert.AreEqual(_character.Width, 0);
            Assert.AreEqual(_character.Height, 0);
            Assert.AreEqual(_character.XOffset, 0);
            Assert.AreEqual(_character.YOffset, 0);
            Assert.AreEqual(_character.XAdvance, 0);
            Assert.AreEqual(_character.Page, 0);
            Assert.AreEqual(_character.Channel, Channel.All);
        }

        [Test]
        public void TestEqualsAndHashCode()
        {
            var x = new Character();
            var y = new Character();
            Assert.IsTrue(x.Equals(y) && y.Equals(x));
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }
    }
}