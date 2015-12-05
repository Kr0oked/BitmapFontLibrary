using System.Xml;
using System.Xml.Schema;
using BitmapFontLibrary.Loader.Parser.Xml;
using Moq;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Loader.Parser.Xml
{
    [TestFixture]
    public class XmlSettingsBuilderTest
    {
        private XmlSettingsBuilder _xmlSettingsBuilder;
        private Mock<IXmlSchemaReader> _xmlSchemaReader;

        [SetUp]
        public void Initialize()
        {
            _xmlSchemaReader = new Mock<IXmlSchemaReader>();
            _xmlSettingsBuilder = new XmlSettingsBuilder(_xmlSchemaReader.Object);
        }

        [Test]
        public void TestBuildXmlReaderSettingsWithEnabledXmlValidation()
        {
            var schema = new XmlSchema();
            _xmlSchemaReader
                .Setup(reader => reader.GetXmlSchema(@"Data\Xsd\BitmapFont.xsd"))
                .Returns(schema);

            var settings = _xmlSettingsBuilder.BuildXmlReaderSettings(true);

            Assert.AreEqual(settings.ValidationType, ValidationType.Schema);
            Assert.IsTrue(settings.Schemas.Contains(schema));
        }

        [Test]
        public void TestBuildXmlReaderSettingsWithDisabledXmlValidation()
        {
            var settings = _xmlSettingsBuilder.BuildXmlReaderSettings(false);
            Assert.AreEqual(settings.ValidationType, ValidationType.None);
        }
    }
}