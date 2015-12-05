using System;
using System.IO;
using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Loader.Parser.Text;
using Moq;
using NUnit.Framework;
using TextReader = BitmapFontLibrary.Loader.Parser.Text.TextReader;

namespace BitmapFontLibraryTest.Loader.Parser.Text
{
    [TestFixture]
    public class TextReaderTest
    {
        private TextReader _reader;
        private Mock<IStringAdapter> _stringAdapter;

        [SetUp]
        public void Initialize()
        {
            _stringAdapter = new Mock<IStringAdapter>();
            _stringAdapter
                .Setup(adapter => adapter.CommaSeparatedIntStringToIntArray("0,1,2,3"))
                .Returns(new[] {0, 1, 2, 3});
            _stringAdapter
                .Setup(adapter => adapter.CommaSeparatedIntStringToIntArray("notCommaSeparatedIntegers"))
                .Throws(new FormatException());

            _reader = new TextReader(_stringAdapter.Object);
        }

        [Test]
        public void TestInitialize()
        {
            _reader.Initialize(GetStream(""));
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
            Assert.AreEqual(_reader.Name, "");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInitializeThrowsArgumentNullException()
        {
            _reader.Initialize(null);
        }

        [Test]
        public void TestReadTag()
        {
            _reader.Initialize(GetStream("testTag testAttribute=0"));
            Assert.IsTrue(_reader.ReadTag());
            Assert.AreEqual(_reader.ElementType, TextElementType.Attribute);
            Assert.AreEqual(_reader.Name, "testTag");
        }

        [Test]
        [ExpectedException(typeof (FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestReadTagThrowsFieldAccessException()
        {
            _reader.ReadTag();
        }

        [Test]
        [ExpectedException(typeof(MethodAccessException), ExpectedMessage = "Cannot read tag name. Element Type is 'Value'")]
        public void TestReadTagThrowsMethodAccessException()
        {
            _reader.Initialize(GetStream("testTag testAttribute=0"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            _reader.ReadTag();
        }

        [Test]
        public void TestReadTagReachesEndOfStream()
        {
            _reader.Initialize(GetStream("testTag"));
            Assert.IsFalse(_reader.ReadTag());
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
            Assert.AreEqual(_reader.Name, "testTag");
        }

        [Test]
        public void TestReadTagReachesEndOfLine()
        {
            _reader.Initialize(GetStream("testTag\r\n "));
            Assert.IsTrue(_reader.ReadTag());
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
            Assert.AreEqual(_reader.Name, "testTag");
        }

        [Test]
        public void TestMoveToNextTag()
        {
            _reader.Initialize(GetStream("testTagOne testAttributeOne=0\r\ntestTagTwo testAttributeTwo=1"));
            _reader.MoveToNextTag();
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
            Assert.IsTrue(_reader.ReadTag());
            Assert.AreEqual(_reader.ElementType, TextElementType.Attribute);
            Assert.AreEqual(_reader.Name, "testTagTwo");
        }

        [Test]
        [ExpectedException(typeof(FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestMoveToNextTagThrowsFieldAccessException()
        {
            _reader.MoveToNextTag();
        }

        [Test]
        public void TestMoveToNextTagReachesEndOfStream()
        {
            _reader.Initialize(GetStream("testTagOne"));
            Assert.IsFalse(_reader.MoveToNextTag());
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadAttribute()
        {
            _reader.Initialize(GetStream("testTag    testAttribute=0"));
            _reader.ReadTag();
            Assert.IsTrue(_reader.ReadAttribute());
            Assert.AreEqual(_reader.ElementType, TextElementType.Value);
            Assert.AreEqual(_reader.Name, "testAttribute");
        }

        [Test]
        [ExpectedException(typeof(FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestReadAttributeThrowsFieldAccessException()
        {
            _reader.ReadAttribute();
        }

        [Test]
        [ExpectedException(typeof(MethodAccessException), ExpectedMessage = "Cannot read attribute name. Element Type is 'Tag'")]
        public void TestReadAttributeThrowsMethodAccessException()
        {
            _reader.Initialize(GetStream(""));
            _reader.ReadAttribute();
        }

        [Test]
        public void TestReadAttributeReachesEndOfStream()
        {
            _reader.Initialize(GetStream("testTag testAttribute"));
            _reader.ReadTag();
            Assert.IsFalse(_reader.ReadAttribute());
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
            Assert.AreEqual(_reader.Name, "testAttribute");
        }

        [Test]
        public void TestReadElementReachesEndOfLine()
        {
            _reader.Initialize(GetStream("testTag testAttribute\r\n "));
            _reader.ReadTag();
            Assert.IsTrue(_reader.ReadAttribute());
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
            Assert.AreEqual(_reader.Name, "testAttribute");
        }

        [Test]
        public void TestReadValueAsString()
        {
            _reader.Initialize(GetStream(@"testTag testAttributeOne=    ""testStringValue"" testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsString(), "testStringValue");
            Assert.AreEqual(_reader.ElementType, TextElementType.Attribute);
        }

        [Test]
        [ExpectedException(typeof(FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestReadValueAsStringThrowsFieldAccessException()
        {
            _reader.ReadValueAsString();
        }

        [Test]
        [ExpectedException(typeof(MethodAccessException), ExpectedMessage = "Cannot read attribute value. Element Type is 'Tag'")]
        public void TestReadValueAsStringThrowsMethodAccessException()
        {
            _reader.Initialize(GetStream(""));
            _reader.ReadValueAsString();
        }

        [Test]
        [ExpectedException(typeof(FormatException), ExpectedMessage = "Expected double quote, got 'n'")]
        public void TestReadValueAsStringThrowsFormatException()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne= notADoubleQuote testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            _reader.ReadValueAsString();
        }

        [Test]
        public void TestReadValueAsStringReachesEndOfStream()
        {
            _reader.Initialize(GetStream(@"testTag testAttributeOne=""testStringValue"""));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsString(), "testStringValue");
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadValueAsStringReachesEndOfLine()
        {
            _reader.Initialize(GetStream(@"testTag testAttributeOne=""testStringValue"""));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsString(), "testStringValue");
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadValueAsInteger()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=    0 testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsInteger(), 0);
            Assert.AreEqual(_reader.ElementType, TextElementType.Attribute);
        }

        [Test]
        [ExpectedException(typeof(FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestReadValueAsIntegerThrowsFieldAccessException()
        {
            _reader.ReadValueAsInteger();
        }

        [Test]
        [ExpectedException(typeof(MethodAccessException), ExpectedMessage = "Cannot read attribute value. Element Type is 'Tag'")]
        public void TestReadValueAsIntegerThrowsMethodAccessException()
        {
            _reader.Initialize(GetStream(""));
            _reader.ReadValueAsInteger();
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void TestReadValueAsIntegerThrowsFormatException()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=notANumber testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            _reader.ReadValueAsInteger();
        }

        [Test]
        public void TestReadValueAsIntegerReachesEndOfStream()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=0"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsInteger(), 0);
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadValueAsIntegerReachesEndOfLine()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=0\r\n "));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsInteger(), 0);
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadValueAsBoolean()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=    0 testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsBoolean(), false);
            Assert.AreEqual(_reader.ElementType, TextElementType.Attribute);
        }

        [Test]
        [ExpectedException(typeof(FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestReadValueAsBooleanThrowsFieldAccessException()
        {
            _reader.ReadValueAsBoolean();
        }

        [Test]
        [ExpectedException(typeof(MethodAccessException), ExpectedMessage = "Cannot read attribute value. Element Type is 'Tag'")]
        public void TestReadValueAsBooleanThrowsMethodAccessException()
        {
            _reader.Initialize(GetStream(""));
            _reader.ReadValueAsBoolean();
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void TestReadValueAsBooleanThrowsFormatException()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=notANumber testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            _reader.ReadValueAsBoolean();
        }

        [Test]
        public void TestReadValueAsBooleanReachesEndOfStream()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsBoolean(), true);
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadValueAsBooleanReachesEndOfLine()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=2\r\n "));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsBoolean(), true);
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadValueAsCommaSeparatedIntegers()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=    0,1,2,3 testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsCommaSeparatedIntegers(), new[] {0, 1, 2, 3});
            Assert.AreEqual(_reader.ElementType, TextElementType.Attribute);
        }

        [Test]
        [ExpectedException(typeof(FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestReadValueAsCommaSeparatedIntegersThrowsFieldAccessException()
        {
            _reader.ReadValueAsCommaSeparatedIntegers();
        }

        [Test]
        [ExpectedException(typeof(MethodAccessException), ExpectedMessage = "Cannot read attribute value. Element Type is 'Tag'")]
        public void TestReadValueAsCommaSeparatedIntegersThrowsMethodAccessException()
        {
            _reader.Initialize(GetStream(""));
            _reader.ReadValueAsCommaSeparatedIntegers();
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void TestReadValueAsCommaSeparatedIntegersThrowsFormatException()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=notCommaSeparatedIntegers testAttributeTwo=1"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            _reader.ReadValueAsCommaSeparatedIntegers();
        }

        [Test]
        public void TestReadValueAsCommaSeparatedIntegersReachesEndOfStream()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=0,1,2,3"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsCommaSeparatedIntegers(), new[] {0, 1, 2, 3});
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        public void TestReadValueAsCommaSeparatedIntegersReachesEndOfLine()
        {
            _reader.Initialize(GetStream("testTag testAttributeOne=0,1,2,3\r\n "));
            _reader.ReadTag();
            _reader.ReadAttribute();
            Assert.AreEqual(_reader.ReadValueAsCommaSeparatedIntegers(), new[] {0, 1, 2, 3});
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
        }

        [Test]
        [ExpectedException(typeof(FieldAccessException), ExpectedMessage = "Class is not initialized")]
        public void TestClose()
        {
            _reader.Initialize(GetStream("testTag testAttribute=0"));
            _reader.ReadTag();
            _reader.ReadAttribute();
            _reader.Close();
            Assert.AreEqual(_reader.ElementType, TextElementType.Tag);
            Assert.AreEqual(_reader.Name, "");
            _reader.Close();
        }

        private static Stream GetStream(string streamContent)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(streamContent);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}