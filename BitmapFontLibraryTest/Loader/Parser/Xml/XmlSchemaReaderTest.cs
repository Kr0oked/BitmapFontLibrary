using System.IO;
using System.Xml.Schema;
using BitmapFontLibrary.Loader.Parser.Xml;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Loader.Parser.Xml
{
    [TestFixture]
    public class XmlSchemaReaderTest
    {
        private XmlSchemaReader _reader;

        [SetUp]
        public void Initialize()
        {
            _reader = new XmlSchemaReader();
        }

        [Test]
        public void TestGetXmlSchema()
        {
            Assert.IsInstanceOf(typeof (XmlSchema), _reader.GetXmlSchema(@"Data\Xsd\testSchema.xsd"));
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestGetXmlSchemaThrowsFileNotFoundException()
        {
            _reader.GetXmlSchema(@"Data\Xsd\notExistentFile.xsd");
        }
    }
}