using BitmapFontLibrary.Model;
using Moq;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Model
{
    [TestFixture]
    public class FontTest
    {
        private Font _font;

        [SetUp]
        public void Initialize()
        {
            _font = new Font();
        }

        [Test]
        public void TestGetAndAddPage()
        {
            var fontTexture = new Mock<IFontTexture>();
            _font.AddPage(0, fontTexture.Object);
            Assert.AreEqual(fontTexture.Object, _font.GetPage(0));
        }

        [Test]
        public void TestGetPageWithInvalidPageId()
        {
            Assert.IsNull(_font.GetPage(0));
        }

        [Test]
        public void TestGetCharacterByIdAndAddCharacter()
        {
            var character = new Character();
            _font.AddCharacter(3, character);
            Assert.AreEqual(character, _font.GetCharacterById(3));
        }

        [Test]
        public void TestGetCharacterByIdWithInvalidCharacterId()
        {
            Assert.IsNull(_font.GetCharacterById(0));
        }

        [Test]
        public void TestGetCharacterByIndexAndAddCharacter()
        {
            var character = new Character();
            _font.AddCharacter(3, character);
            Assert.AreEqual(character, _font.GetCharacterByIndex(0));
        }

        [Test]
        public void TestGetCharacterByIndexWithInvalidCharacterIndex()
        {
            Assert.IsNull(_font.GetCharacterByIndex(0));
        }

        [Test]
        public void TestGetCharacterIndex()
        {
            var firstCharacter = new Character();
            var secondCharacter = new Character();
            _font.AddCharacter(0, firstCharacter);
            _font.AddCharacter(100, secondCharacter);
            Assert.AreEqual(0, _font.GetCharacterIndex(0));
            Assert.AreEqual(1, _font.GetCharacterIndex(100));
        }

        [Test]
        public void TestGetCharacterSize()
        {
            var firstCharacter = new Character();
            var secondCharacter = new Character();
            _font.AddCharacter(0, firstCharacter);
            _font.AddCharacter(100, secondCharacter);
            Assert.AreEqual(2, _font.GetCharactersSize());
        }

        [Test]
        public void TestGetAndAddKerning()
        {
            _font.AddKerning(0, 0, 1);
            Assert.AreEqual(1, _font.GetKerningAmount(0, 0));
        }

        [Test]
        public void TestGetKerningWithInvalidFirstCharacterId()
        {
            Assert.AreEqual(0, _font.GetKerningAmount(0, 0));
        }

        [Test]
        public void TestGetKerningWithInvalidSecondCharacterId()
        {
            _font.AddKerning(0, 0, 1);
            Assert.AreEqual(0, _font.GetKerningAmount(0, 1));
        }
    }
}
