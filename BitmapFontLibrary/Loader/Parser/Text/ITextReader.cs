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

using System.IO;

namespace BitmapFontLibrary.Loader.Parser.Text
{
    /// <summary>
    /// Interface for readers of text files in the format used in the Angelcode Bitmap Font files.
    /// </summary>
    public interface ITextReader
    {
        /// <summary>
        /// Element type at the current position of the reader.
        /// </summary>
        TextElementType ElementType { get; }

        /// <summary>
        /// Name of the current tag or attribute.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Initalizes the reader.
        /// </summary>
        /// <param name="inputStream"></param>
        void Initialize(Stream inputStream);

        /// <summary>
        /// Reads the tag name.
        /// </summary>
        /// <returns>false if the reader reached the end of the stream, otherwise true</returns>
        bool ReadTag();

        /// <summary>
        /// Moves to the next tag.
        /// </summary>
        /// <returns>false if the reader reached the end of the stream, otherwise true</returns>
        bool MoveToNextTag();

        /// <summary>
        /// Reads the next attribute.
        /// </summary>
        /// <returns>false if the reader reached the end of the stream, otherwise true</returns>
        bool ReadAttribute();

        /// <summary>
        /// Reads a value as a string.
        /// </summary>
        /// <returns>The string value</returns>
        string ReadValueAsString();

        /// <summary>
        /// Reads a value as an integer.
        /// </summary>
        /// <returns>The integer value</returns>
        int ReadValueAsInteger();

        /// <summary>
        /// Reads a value as a boolean.
        /// </summary>
        /// <returns>The boolean value</returns>
        bool ReadValueAsBoolean();

        /// <summary>
        /// Reads a value as comma separated integers.
        /// </summary>
        /// <returns>The integers as array</returns>
        int[] ReadValueAsCommaSeparatedIntegers();

        /// <summary>
        /// Closes the stream and terminates the reader.
        /// </summary>
        void Close();
    }
}