#region License
//
// The MIT License (MIT)
//
// Copyright (c) 2015 Philipp Bobek
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using BitmapFontLibrary.Helper;

namespace BitmapFontLibrary.Loader.Parser.Text
{
    /// <summary>
    /// Reader of text files in the format used in the Angelcode Bitmap Font files.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class TextReader : ITextReader
    {
        private readonly IStringAdapter _stringAdapter;
        private StreamReader _reader;

        /// <summary>
        /// Element type at the current position of the reader.
        /// </summary>
        public TextElementType ElementType { get; private set; }

        /// <summary>
        /// Name of the current tag or attribute.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Reader of text files in the format used in the Angelcode Bitmap Font files.
        /// </summary>
        /// <param name="stringAdapter">Object of a class that implements the IStringAdapter interface</param>
        public TextReader(IStringAdapter stringAdapter)
        {
            if (stringAdapter == null) throw new ArgumentNullException("stringAdapter");
            _stringAdapter = stringAdapter;
            ElementType = TextElementType.Tag;
            Name = "";
        }

        /// <summary>
        /// Initalizes the reader.
        /// </summary>
        /// <param name="inputStream"></param>
        public void Initialize(Stream inputStream)
        {
            ElementType = TextElementType.Tag;
            Name = "";
            _reader = new StreamReader(inputStream);
        }

        /// <summary>
        /// Reads the tag name.
        /// </summary>
        /// <returns>false if the reader reached the end of the stream, otherwise true</returns>
        public bool ReadTag()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");
            if (ElementType != TextElementType.Tag) throw new MethodAccessException("Cannot read tag name. Element Type is '" + ElementType + "'");

            var tag = "";
            int characterValue;

            while (_reader.Peek() == ' ') _reader.Read();

            while ((characterValue = _reader.Read()) != -1)
            {
                var character = Convert.ToChar(characterValue);

                if (character == '\r' && Convert.ToChar(_reader.Peek()) == '\n')
                {
                    _reader.Read();
                    break;
                }

                if (character == ' ')
                {
                    ElementType = TextElementType.Attribute;
                    break;
                }

                tag += character;
            }

            Name = tag;
            return !_reader.EndOfStream;
        }

        /// <summary>
        /// Moves to the next tag.
        /// </summary>
        /// <returns>false if the reader reached the end of the stream, otherwise true</returns>
        public bool MoveToNextTag()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");

            int characterValue;

            while ((characterValue = _reader.Read()) != -1)
            {
                var character = Convert.ToChar(characterValue);

                if (character != '\r' || Convert.ToChar(_reader.Peek()) != '\n') continue;
                _reader.Read();
                ElementType = TextElementType.Tag;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads the next attribute.
        /// </summary>
        /// <returns>false if the reader reached the end of the stream, otherwise true</returns>
        public bool ReadAttribute()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");
            if (ElementType != TextElementType.Attribute) throw new MethodAccessException("Cannot read attribute name. Element Type is '" + ElementType + "'");

            var attribute = "";
            int characterValue;

            while (_reader.Peek() == ' ') _reader.Read();

            while ((characterValue = _reader.Read()) != -1)
            {
                var character = Convert.ToChar(characterValue);

                if (character == '\r' && Convert.ToChar(_reader.Peek()) == '\n')
                {
                    _reader.Read();
                    ElementType = TextElementType.Tag;
                    break;
                }

                if (character == '=')
                {
                    ElementType = TextElementType.Value;
                    break;
                }

                attribute += character;
            }

            Name = attribute;
            if (_reader.EndOfStream)
            {
                ElementType = TextElementType.Tag;
            }
            return !_reader.EndOfStream;
        }

        /// <summary>
        /// Reads a value as a string.
        /// </summary>
        /// <returns>The string value</returns>
        public string ReadValueAsString()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");
            if (ElementType != TextElementType.Value) throw new MethodAccessException("Cannot read attribute value. Element Type is '" + ElementType + "'");

            var stringValue = "";
            int characterValue;

            while (_reader.Peek() == ' ') _reader.Read();

            if ((characterValue = _reader.Read()) != '"') throw new FormatException("Expected double quote, got '" + Convert.ToChar(characterValue) + "'");

            while ((characterValue = _reader.Read()) != -1)
            {
                var character = Convert.ToChar(characterValue);

                if (character == '\r' && Convert.ToChar(_reader.Peek()) == '\n')
                {
                    _reader.Read();
                    ElementType = TextElementType.Tag;
                    break;
                }

                if (character == '"')
                {
                    ElementType = TextElementType.Attribute;
                    break;
                }

                stringValue += character;
            }

            if (_reader.EndOfStream)
            {
                ElementType = TextElementType.Tag;
            }
            return stringValue;
        }

        /// <summary>
        /// Reads a value as an integer.
        /// </summary>
        /// <returns>The integer value</returns>
        public int ReadValueAsInteger()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");
            if (ElementType != TextElementType.Value) throw new MethodAccessException("Cannot read attribute value. Element Type is '" + ElementType + "'");

            var stringValue = "";
            int characterValue;

            while (_reader.Peek() == ' ') _reader.Read();

            while ((characterValue = _reader.Read()) != -1)
            {
                var character = Convert.ToChar(characterValue);

                if (character == '\r' && Convert.ToChar(_reader.Peek()) == '\n')
                {
                    _reader.Read();
                    ElementType = TextElementType.Tag;
                    break;
                }

                if (character == ' ')
                {
                    ElementType = TextElementType.Attribute;
                    break;
                }

                stringValue += character;
            }

            if (_reader.EndOfStream)
            {
                ElementType = TextElementType.Tag;
            }
            return Convert.ToInt32(stringValue);
        }

        /// <summary>
        /// Reads a value as a boolean.
        /// </summary>
        /// <returns>The boolean value</returns>
        public bool ReadValueAsBoolean()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");
            if (ElementType != TextElementType.Value) throw new MethodAccessException("Cannot read attribute value. Element Type is '" + ElementType + "'");

            var stringValue = "";
            int characterValue;

            while (_reader.Peek() == ' ') _reader.Read();

            while ((characterValue = _reader.Read()) != -1)
            {
                var character = Convert.ToChar(characterValue);

                if (character == '\r' && Convert.ToChar(_reader.Peek()) == '\n')
                {
                    _reader.Read();
                    ElementType = TextElementType.Tag;
                    break;
                }

                if (character == ' ')
                {
                    ElementType = TextElementType.Attribute;
                    break;
                }

                stringValue += character;
            }

            if (_reader.EndOfStream)
            {
                ElementType = TextElementType.Tag;
            }
            return Convert.ToBoolean(Convert.ToInt32(stringValue));
        }

        /// <summary>
        /// Reads a value as comma separated integers.
        /// </summary>
        /// <returns>The integers as array</returns>
        public int[] ReadValueAsCommaSeparatedIntegers()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");
            if (ElementType != TextElementType.Value) throw new MethodAccessException("Cannot read attribute value. Element Type is '" + ElementType + "'");

            var stringValue = "";
            int characterValue;

            while (_reader.Peek() == ' ') _reader.Read();

            while ((characterValue = _reader.Read()) != -1)
            {
                var character = Convert.ToChar(characterValue);

                if (character == '\r' && Convert.ToChar(_reader.Peek()) == '\n')
                {
                    _reader.Read();
                    ElementType = TextElementType.Tag;
                    break;
                }

                if (character == ' ')
                {
                    ElementType = TextElementType.Attribute;
                    break;
                }

                stringValue += character;
            }

            if (_reader.EndOfStream)
            {
                ElementType = TextElementType.Tag;
            }
            return _stringAdapter.CommaSeparatedIntStringToIntArray(stringValue);
        }

        /// <summary>
        /// Closes the stream and terminates the reader.
        /// </summary>
        public void Close()
        {
            if (_reader == null) throw new FieldAccessException("Class is not initialized");
            ElementType = TextElementType.Tag;
            Name = "";
            _reader.Close();
            _reader = null;
        }
    }
}