using BitmapFontLibrary;
using BitmapFontLibrary.Loader;
using BitmapFontLibrary.Model;
using BitmapFontLibrary.Renderer;
using Moq;
using NUnit.Framework;

namespace BitmapFontLibraryTest
{
    [TestFixture]
    public class BitmapFontTest
    {
        private BitmapFont _bitmapFont;
        private Mock<IFontLoader> _fontLoader;
        private Mock<IFontRenderer> _fontRenderer;
        private Mock<ITextConfiguration> _textConfiguration;

        [SetUp]
        public void Initialize()
        {
            _fontLoader = new Mock<IFontLoader>();
            _fontRenderer = new Mock<IFontRenderer>();
            _textConfiguration = new Mock<ITextConfiguration>();
            _bitmapFont = new BitmapFont(_fontLoader.Object, _fontRenderer.Object);
        }

        [Test]
        public void TestInitializeAndDraw()
        {
            const string path = "pathToTheFontFile";
            var font = new Font();
            const string text = "textToDraw";
            const float x = 1.0f;
            const float y = 2.0f;
            const float z = 3.0f;

            _fontLoader
                .Setup(loader => loader.Load(path))
                .Returns(font);

            _bitmapFont.Initialize(path);

            _fontRenderer
                .Verify(renderer => renderer.Initialize(font));

            _bitmapFont.Draw(text, x, y, z, _textConfiguration.Object);

            _fontRenderer
                 .Verify(renderer => renderer.Render(text, x, y, z, _textConfiguration.Object));
        }

        [Test]
        public void TestInitializeAndDrawWithTheStandardConfiguration()
        {
            const string path = "pathToTheFontFile";
            var font = new Font();
            const string text = "textToDraw";
            const float x = 1.0f;
            const float y = 2.0f;
            const float z = 3.0f;

            _fontLoader
                .Setup(loader => loader.Load(path))
                .Returns(font);

            _bitmapFont.Initialize(path);

            _fontRenderer
                .Verify(renderer => renderer.Initialize(font));

            _bitmapFont.Draw(text, x, y, z);

            _fontRenderer
                 .Verify(renderer => renderer.Render(text, x, y, z, It.IsAny<ITextConfiguration>()));
        }
    }
}
