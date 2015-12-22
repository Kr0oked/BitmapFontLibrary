using System.Collections;
using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Model;
using BitmapFontLibrary.Renderer;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Renderer
{
    [TestFixture]
    public class LineCalculatorTest
    {
        private LineCalculator _calculator;

        [SetUp]
        public void Initialize()
        {
            _calculator = new LineCalculator(new CharAdapter());
        }

        [Test]
        public void TestCalculateLines()
        {
            var textConfiguration = new TextConfiguration
            {
                MaximalWidth = 100,
                Alignment = TextAlignment.Centered,
                SizeInPixels = 16
            };

            var expectedLines = new ArrayList
            {
                new Line()
                {
                    Text = "AV AVAV AVAV",
                    X = 3.0f,
                    Y = 0.0f
                }, 
                new Line()
                {
                    Text = "AV AVAVAVAV",
                    X = 5.5f,
                    Y = 29.0f
                }
            };

            var actualLines = _calculator.CalculateLines("AV AVAV AVAVAV AVAVAVAV", textConfiguration, GetTestFont());

            Assert.AreEqual(actualLines.Count, 2);
            Assert.AreEqual(actualLines[0], expectedLines[0]);
            Assert.AreEqual(actualLines[1], expectedLines[1]);
        }

        private static IFont GetTestFont()
        {
            var characterA = new Character
            {
                Width = 20,
                XOffset = 0,
                XAdvance = 19
            };

            var characterV = new Character
            {
                Width = 19,
                XOffset = 0,
                XAdvance = 19
            };

            var characterSpace = new Character
            {
                Width = 1,
                XOffset = 0,
                XAdvance = 6
            };

            var font = new Font
            {
                Size = 32,
                LineHeight = 32,
                BaseHeight = 26,
                PagesCount = 1
            };

            font.AddCharacter(32, characterSpace);
            font.AddCharacter(65, characterA);
            font.AddCharacter(86, characterV);
            font.AddKerning(86, 65, -2);
            font.AddKerning(65, 86, -2);

            return font;
        }
    }
}