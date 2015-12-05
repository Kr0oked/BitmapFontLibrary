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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using BitmapFontLibrary.Loader.Exception;

namespace BitmapFontLibrary.Loader.Parser.Xml
{
    /// <summary>
    /// Reader for xml schemas.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class XmlSchemaReader : IXmlSchemaReader
    {
        private readonly string _assemblyDirectory;

        /// <summary>
        /// Reader for xml schemas.
        /// </summary>
        public XmlSchemaReader()
        {
            _assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (_assemblyDirectory == null) throw new FontLoaderException("Cant find Assembly Directory");
        }

        /// <summary>
        /// Reads a xml schema from a file.
        /// </summary>
        /// <param name="pathRelativeToAssembly">The path to the xml schema file relative to the assembly directory</param>
        /// <returns>The read xml schema</returns>
        public XmlSchema GetXmlSchema(string pathRelativeToAssembly)
        {
            var schemaReader = new XmlTextReader(Path.Combine(_assemblyDirectory, pathRelativeToAssembly));
            return XmlSchema.Read(schemaReader, ValidationCallback);
        }

        /// <summary>
        /// Handler for errors in the xml syntax.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The arguments of the event</param>
        private static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            throw new FontLoaderException(args.Message);
        }
    }
}