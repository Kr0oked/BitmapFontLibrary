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
using System.IO;
using BitmapFontLibrary.Loader.Exception;
using BitmapFontLibrary.Loader.Parser;
using BitmapFontLibrary.Model;
using Ninject;

namespace BitmapFontLibrary.Loader
{
    /// <summary>
    /// Loader for fonts.
    /// </summary>
    public class FontLoader : IFontLoader
    {
        private readonly IFontFileParser _binaryFontFileParser;
        private readonly IFontFileParser _textFontFileParser;
        private readonly IFontFileParser _xmlFontFileParser;

        /// <summary>
        /// Loader for fonts.
        /// </summary>
        /// <param name="binaryFontFileParser">Object of a class that implements the IFontFileParser interface</param>
        /// <param name="textFontFileParser">Object of a class that implements the IFontFileParser interface</param>
        /// <param name="xmlFontFileParser">Object of a class that implements the IFontFileParser interface</param>
        public FontLoader([Named("Binary")] IFontFileParser binaryFontFileParser, [Named("Text")] IFontFileParser textFontFileParser, 
            [Named("Xml")] IFontFileParser xmlFontFileParser)
        {
            if (binaryFontFileParser == null) throw new ArgumentNullException("binaryFontFileParser");
            if (textFontFileParser == null) throw new ArgumentNullException("textFontFileParser");
            if (xmlFontFileParser == null) throw new ArgumentNullException("xmlFontFileParser");
            _binaryFontFileParser = binaryFontFileParser;
            _textFontFileParser = textFontFileParser;
            _xmlFontFileParser = xmlFontFileParser;
        }

        /// <summary>
        /// Loads a font from an Angelcode Bitmap Font file.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>The loaded font</returns>
        public Font Load(string path)
        {
            try
            {
                IFontFileParser fontFileParser;
                switch (Path.GetExtension(path))
                {
                    case ".fnt":
                        fontFileParser = _binaryFontFileParser;
                        break;
                    case ".txt":
                        fontFileParser = _textFontFileParser;
                        break;
                    case ".xml":
                        fontFileParser = _xmlFontFileParser;
                        break;
                    default:
                        throw new ArgumentException("Unsupported extension: " + Path.GetExtension(path));
                }

                return fontFileParser.Parse(new FileStream(path, FileMode.Open, FileAccess.Read), Path.GetDirectoryName(path));
            }
            catch (System.Exception exception)
            {
                throw new FontLoaderException("Failed loading a font", exception);
            }
        }
    }
}