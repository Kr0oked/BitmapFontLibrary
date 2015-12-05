using BitmapFontLibrary.Helper;
using NUnit.Framework;

namespace BitmapFontLibraryTest.Helper
{
    [TestFixture]
    public class CharAdapterTest
    {
        private CharAdapter _adapter;

        [SetUp]
        public void Initialize()
        {
            _adapter = new CharAdapter();
        }

        [Test]
        public void TestCharToIntCharValue()
        {
            Assert.AreEqual(_adapter.CharToIntCharValue(' '), 32);
            Assert.AreEqual(_adapter.CharToIntCharValue('!'), 33);
            Assert.AreEqual(_adapter.CharToIntCharValue('"'), 34);
            Assert.AreEqual(_adapter.CharToIntCharValue('#'), 35);
            Assert.AreEqual(_adapter.CharToIntCharValue('$'), 36);
            Assert.AreEqual(_adapter.CharToIntCharValue('%'), 37);
            Assert.AreEqual(_adapter.CharToIntCharValue('&'), 38);
            Assert.AreEqual(_adapter.CharToIntCharValue('\''), 39);
            Assert.AreEqual(_adapter.CharToIntCharValue('('), 40);
            Assert.AreEqual(_adapter.CharToIntCharValue(')'), 41);
            Assert.AreEqual(_adapter.CharToIntCharValue('*'), 42);
            Assert.AreEqual(_adapter.CharToIntCharValue('+'), 43);
            Assert.AreEqual(_adapter.CharToIntCharValue(','), 44);
            Assert.AreEqual(_adapter.CharToIntCharValue('-'), 45);
            Assert.AreEqual(_adapter.CharToIntCharValue('.'), 46);
            Assert.AreEqual(_adapter.CharToIntCharValue('/'), 47);
            Assert.AreEqual(_adapter.CharToIntCharValue('0'), 48);
            Assert.AreEqual(_adapter.CharToIntCharValue('1'), 49);
            Assert.AreEqual(_adapter.CharToIntCharValue('2'), 50);
            Assert.AreEqual(_adapter.CharToIntCharValue('3'), 51);
            Assert.AreEqual(_adapter.CharToIntCharValue('4'), 52);
            Assert.AreEqual(_adapter.CharToIntCharValue('5'), 53);
            Assert.AreEqual(_adapter.CharToIntCharValue('6'), 54);
            Assert.AreEqual(_adapter.CharToIntCharValue('7'), 55);
            Assert.AreEqual(_adapter.CharToIntCharValue('8'), 56);
            Assert.AreEqual(_adapter.CharToIntCharValue('9'), 57);
            Assert.AreEqual(_adapter.CharToIntCharValue(':'), 58);
            Assert.AreEqual(_adapter.CharToIntCharValue(';'), 59);
            Assert.AreEqual(_adapter.CharToIntCharValue('<'), 60);
            Assert.AreEqual(_adapter.CharToIntCharValue('='), 61);
            Assert.AreEqual(_adapter.CharToIntCharValue('>'), 62);
            Assert.AreEqual(_adapter.CharToIntCharValue('?'), 63);
            Assert.AreEqual(_adapter.CharToIntCharValue('@'), 64);
            Assert.AreEqual(_adapter.CharToIntCharValue('A'), 65);
            Assert.AreEqual(_adapter.CharToIntCharValue('B'), 66);
            Assert.AreEqual(_adapter.CharToIntCharValue('C'), 67);
            Assert.AreEqual(_adapter.CharToIntCharValue('D'), 68);
            Assert.AreEqual(_adapter.CharToIntCharValue('E'), 69);
            Assert.AreEqual(_adapter.CharToIntCharValue('F'), 70);
            Assert.AreEqual(_adapter.CharToIntCharValue('G'), 71);
            Assert.AreEqual(_adapter.CharToIntCharValue('H'), 72);
            Assert.AreEqual(_adapter.CharToIntCharValue('I'), 73);
            Assert.AreEqual(_adapter.CharToIntCharValue('J'), 74);
            Assert.AreEqual(_adapter.CharToIntCharValue('K'), 75);
            Assert.AreEqual(_adapter.CharToIntCharValue('L'), 76);
            Assert.AreEqual(_adapter.CharToIntCharValue('M'), 77);
            Assert.AreEqual(_adapter.CharToIntCharValue('N'), 78);
            Assert.AreEqual(_adapter.CharToIntCharValue('O'), 79);
            Assert.AreEqual(_adapter.CharToIntCharValue('P'), 80);
            Assert.AreEqual(_adapter.CharToIntCharValue('Q'), 81);
            Assert.AreEqual(_adapter.CharToIntCharValue('R'), 82);
            Assert.AreEqual(_adapter.CharToIntCharValue('S'), 83);
            Assert.AreEqual(_adapter.CharToIntCharValue('T'), 84);
            Assert.AreEqual(_adapter.CharToIntCharValue('U'), 85);
            Assert.AreEqual(_adapter.CharToIntCharValue('V'), 86);
            Assert.AreEqual(_adapter.CharToIntCharValue('W'), 87);
            Assert.AreEqual(_adapter.CharToIntCharValue('X'), 88);
            Assert.AreEqual(_adapter.CharToIntCharValue('Y'), 89);
            Assert.AreEqual(_adapter.CharToIntCharValue('Z'), 90);
            Assert.AreEqual(_adapter.CharToIntCharValue('['), 91);
            Assert.AreEqual(_adapter.CharToIntCharValue('\\'), 92);
            Assert.AreEqual(_adapter.CharToIntCharValue(']'), 93);
            Assert.AreEqual(_adapter.CharToIntCharValue('^'), 94);
            Assert.AreEqual(_adapter.CharToIntCharValue('_'), 95);
            Assert.AreEqual(_adapter.CharToIntCharValue('`'), 96);
            Assert.AreEqual(_adapter.CharToIntCharValue('a'), 97);
            Assert.AreEqual(_adapter.CharToIntCharValue('b'), 98);
            Assert.AreEqual(_adapter.CharToIntCharValue('c'), 99);
            Assert.AreEqual(_adapter.CharToIntCharValue('d'), 100);
            Assert.AreEqual(_adapter.CharToIntCharValue('e'), 101);
            Assert.AreEqual(_adapter.CharToIntCharValue('f'), 102);
            Assert.AreEqual(_adapter.CharToIntCharValue('g'), 103);
            Assert.AreEqual(_adapter.CharToIntCharValue('h'), 104);
            Assert.AreEqual(_adapter.CharToIntCharValue('i'), 105);
            Assert.AreEqual(_adapter.CharToIntCharValue('j'), 106);
            Assert.AreEqual(_adapter.CharToIntCharValue('k'), 107);
            Assert.AreEqual(_adapter.CharToIntCharValue('l'), 108);
            Assert.AreEqual(_adapter.CharToIntCharValue('m'), 109);
            Assert.AreEqual(_adapter.CharToIntCharValue('n'), 110);
            Assert.AreEqual(_adapter.CharToIntCharValue('o'), 111);
            Assert.AreEqual(_adapter.CharToIntCharValue('p'), 112);
            Assert.AreEqual(_adapter.CharToIntCharValue('q'), 113);
            Assert.AreEqual(_adapter.CharToIntCharValue('r'), 114);
            Assert.AreEqual(_adapter.CharToIntCharValue('s'), 115);
            Assert.AreEqual(_adapter.CharToIntCharValue('t'), 116);
            Assert.AreEqual(_adapter.CharToIntCharValue('u'), 117);
            Assert.AreEqual(_adapter.CharToIntCharValue('v'), 118);
            Assert.AreEqual(_adapter.CharToIntCharValue('w'), 119);
            Assert.AreEqual(_adapter.CharToIntCharValue('x'), 120);
            Assert.AreEqual(_adapter.CharToIntCharValue('y'), 121);
            Assert.AreEqual(_adapter.CharToIntCharValue('z'), 122);
            Assert.AreEqual(_adapter.CharToIntCharValue('{'), 123);
            Assert.AreEqual(_adapter.CharToIntCharValue('|'), 124);
            Assert.AreEqual(_adapter.CharToIntCharValue('}'), 125);
            Assert.AreEqual(_adapter.CharToIntCharValue('~'), 126);

            Assert.AreEqual(_adapter.CharToIntCharValue('Ä'), 196);
        }
    }
}
