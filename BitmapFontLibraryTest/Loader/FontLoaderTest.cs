using System.IO;
using System.Reflection;
using BitmapFontLibrary.Loader;
using BitmapFontLibrary.Loader.Exception;
using BitmapFontLibrary.Loader.Parser;
using BitmapFontLibrary.Model;
using Moq;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Loader
{
    [TestFixture]
    public class FontLoaderTest
    {
        private FontLoader _fontLoader;
        private Mock<IFontFileParser> _binaryFontLoader;
        private Mock<IFontFileParser> _textFontLoader;
        private Mock<IFontFileParser> _xmlFontLoader;
        private string _assemblyDirectory;

        [SetUp]
        public void Initialize()
        {
            _binaryFontLoader = new Mock<IFontFileParser>();
            _textFontLoader = new Mock<IFontFileParser>();
            _xmlFontLoader = new Mock<IFontFileParser>();
            _fontLoader = new FontLoader(_binaryFontLoader.Object, _textFontLoader.Object, _xmlFontLoader.Object);
            _assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (_assemblyDirectory == null) throw new FileNotFoundException("Assembly");
        }

        [Test]
        public void TestLoadWithBinaryFile()
        {
            var font = new Font();
            var path = Path.Combine(_assemblyDirectory, @"Data\Font\binaryTestFont.fnt");
            _binaryFontLoader
                .Setup(loader => loader.Parse(It.IsAny<Stream>(), Path.GetDirectoryName(path)))
                .Returns(font);
            Assert.AreEqual(font, _fontLoader.Load(path));
        }

        [Test]
        public void TestLoadWithTextFile()
        {
            var font = new Font();
            var path = Path.Combine(_assemblyDirectory, @"Data\Font\textTestFont.txt");
            _textFontLoader
                .Setup(loader => loader.Parse(It.IsAny<Stream>(), Path.GetDirectoryName(path)))
                .Returns(font);
            Assert.AreEqual(font, _fontLoader.Load(path));
        }

        [Test]
        public void TestLoadWithXmlFile()
        {
            var font = new Font();
            var path = Path.Combine(_assemblyDirectory, @"Data\Font\xmlTestFont.xml");
            _xmlFontLoader
                .Setup(loader => loader.Parse(It.IsAny<Stream>(), Path.GetDirectoryName(path))).
                Returns(font);
            Assert.AreEqual(font, _fontLoader.Load(path));
        }

        [Test]
        [ExpectedException(typeof(FontLoaderException), ExpectedMessage = "Failed loading a font")]
        public void TestLoadThrowsFontLoaderExceptionForNullArgument()
        {
            _fontLoader.Load(null);
        }

        [Test]
        [ExpectedException(typeof(FontLoaderException), ExpectedMessage = "Failed loading a font")]
        public void TestLoadThrowsFontLoaderExceptionForUnsupportedFileExtension()
        {
            var path = Path.Combine(_assemblyDirectory, @"Data\textTestFont.unsupported");
            _fontLoader.Load(path);
        }

        [Test]
        [ExpectedException(typeof(FontLoaderException), ExpectedMessage = "Failed loading a font")]
        public void TestLoadThrowsFontLoaderExceptionForInvalidPath()
        {
            var path = Path.Combine(_assemblyDirectory, @"Data\Font\notExistentFile.fnt");
            _fontLoader.Load(path);
        }
    }
}