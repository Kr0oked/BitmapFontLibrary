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
        public void TestInitialValues()
        {
            Assert.AreEqual(_font.Name, "");
            Assert.AreEqual(_font.Size, 0);
            Assert.AreEqual(_font.IsBold, false);
            Assert.AreEqual(_font.IsItalic, false);
            Assert.AreEqual(_font.Charset, "");
            Assert.AreEqual(_font.IsUnicode, true);
            Assert.AreEqual(_font.StretchHeight, 0);
            Assert.AreEqual(_font.IsSmooth, false);
            Assert.AreEqual(_font.SuperSamplingLevel, 0);
            Assert.AreEqual(_font.PaddingTop, 0);
            Assert.AreEqual(_font.PaddingRight, 0);
            Assert.AreEqual(_font.PaddingBottom, 0);
            Assert.AreEqual(_font.PaddingLeft, 0);
            Assert.AreEqual(_font.SpacingTop, 0);
            Assert.AreEqual(_font.SpacingLeft, 0);
            Assert.AreEqual(_font.OutlineThickness, 0);
            Assert.AreEqual(_font.LineHeight, 0);
            Assert.AreEqual(_font.BaseHeight, 0);
            Assert.AreEqual(_font.ScaleWidth, 0);
            Assert.AreEqual(_font.ScaleHeight, 0);
            Assert.AreEqual(_font.PagesCount, 0);
            Assert.AreEqual(_font.AreCharactersPackedInMultipleChannels, false);
            Assert.AreEqual(_font.AlphaChannel, ChannelValue.Outline);
            Assert.AreEqual(_font.RedChannel, ChannelValue.Glyph);
            Assert.AreEqual(_font.GreenChannel, ChannelValue.Glyph);
            Assert.AreEqual(_font.BlueChannel, ChannelValue.Glyph);
        }

        [Test]
        public void TestEquals()
        {
            var texture = new Mock<IFontTexture>().Object;
            var character = new Mock<ICharacter>().Object;

            var x = new Font();
            x.AddPage(0, texture);
            x.AddCharacter(0, character);
            x.AddKerning(0, 0, 0);

            var y = new Font();
            y.AddPage(0, texture);
            y.AddCharacter(0, character);
            y.AddKerning(0, 0, 0);

            Assert.IsTrue(x.Equals(y) && y.Equals(x));
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
