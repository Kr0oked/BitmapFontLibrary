using System.IO;
using System.Reflection;
using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Loader.Parser.Text;
using BitmapFontLibrary.Loader.Texture;
using BitmapFontLibrary.Model;
using Moq;
using NUnit.Framework;
using TextReader = BitmapFontLibrary.Loader.Parser.Text.TextReader;

namespace BitmapFontLibraryTest.Loader.Parser.Text
{
    [TestFixture]
    public class TextFontFileParserTest
    {
        private TextFontFileParser _parser;
        private Mock<IIntAdapter> _intAdapter;
        private Mock<IFontTextureLoader> _fontTextureLoader;
        private Mock<IFontTexture> _fontTexture;
        private string _assemblyDirectory;

        [SetUp]
        public void Initialize()
        {
            _intAdapter = new Mock<IIntAdapter>();
            _fontTextureLoader = new Mock<IFontTextureLoader>();
            _fontTexture = new Mock<IFontTexture>();
            _parser = new TextFontFileParser(new TextReader(new StringAdapter()), _intAdapter.Object, _fontTextureLoader.Object);
            _assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (_assemblyDirectory == null) throw new FileNotFoundException("Assembly");
        }

        [Test]
        public void TestParse()
        {

            _intAdapter
                .Setup(adapter => adapter.IntToEnum<ChannelValue>(0))
                .Returns(ChannelValue.Glyph);

            _intAdapter
                .Setup(adapter => adapter.IntToEnum<ChannelValue>(1))
                .Returns(ChannelValue.Outline);

            _intAdapter
                .Setup(adapter => adapter.IntToEnum<Channel>(15))
                .Returns(Channel.All);

            _fontTextureLoader
                .Setup(loader => loader.Load(Path.Combine(_assemblyDirectory, @"Data\Font\textTestFont_0.png"), true))
                .Returns(_fontTexture.Object);

            var path = Path.Combine(_assemblyDirectory, @"Data\Font\textTestFont.txt");

            var font = _parser.Parse(new FileStream(path, FileMode.Open, FileAccess.Read), Path.GetDirectoryName(path));

            Assert.AreEqual(font, GetExpectedFont());
        }

        private Font GetExpectedFont()
        {
            var characterA = new Character
            {
                X = 0,
                Y = 0,
                Width = 20,
                Height = 21,
                XOffset = 0,
                YOffset = 5,
                XAdvance = 19,
                Page = 0,
                Channel = Channel.All
            };

            var characterV = new Character
            {
                X = 21,
                Y = 0,
                Width = 19,
                Height = 21,
                XOffset = 0,
                YOffset = 5,
                XAdvance = 19,
                Page = 0,
                Channel = Channel.All
            };

            var font = new Font
            {
                Name = "Arial",
                Size = 32,
                IsBold = false,
                IsItalic = false,
                Charset = "",
                IsUnicode = true,
                StretchHeight = 100,
                IsSmooth = true,
                SuperSamplingLevel = 4,
                PaddingTop = 0,
                PaddingRight = 0,
                PaddingBottom = 0,
                PaddingLeft = 0,
                SpacingTop = 1,
                SpacingLeft = 1,
                OutlineThickness = 0,
                LineHeight = 32,
                BaseHeight = 26,
                ScaleWidth = 256,
                ScaleHeight = 256,
                PagesCount = 1,
                AreCharactersPackedInMultipleChannels = false,
                AlphaChannel = ChannelValue.Outline,
                RedChannel = ChannelValue.Glyph,
                GreenChannel = ChannelValue.Glyph,
                BlueChannel = ChannelValue.Glyph
            };

            font.AddPage(0, _fontTexture.Object);
            font.AddCharacter(65, characterA);
            font.AddCharacter(86, characterV);
            font.AddKerning(86, 65, -2);
            font.AddKerning(65, 86, -2);

            return font;
        }
    }
}