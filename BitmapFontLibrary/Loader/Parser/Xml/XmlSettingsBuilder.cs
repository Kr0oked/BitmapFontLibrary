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
using System.Xml;
using System.Xml.Schema;
using BitmapFontLibrary.Loader.Exception;

namespace BitmapFontLibrary.Loader.Parser.Xml
{
    /// <summary>
    /// Builder for xml reader settings.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class XmlSettingsBuilder : IXmlSettingsBuilder
    {
        private readonly IXmlSchemaReader _xmlSchemaReader;

        /// <summary>
        /// Builder for xml reader settings.
        /// </summary>
        /// <param name="xmlSchemaReader">Object of a class that implements the IXmlSchemaReader interface</param>
        public XmlSettingsBuilder(IXmlSchemaReader xmlSchemaReader)
        {
            if (xmlSchemaReader == null) throw new ArgumentNullException("xmlSchemaReader");
            _xmlSchemaReader = xmlSchemaReader;
        }

        /// <summary>
        /// Builds settings for xml readers.
        /// </summary>
        /// <param name="isXmlValidationEnabled">If true the reader will validate the xml with a xml schema</param>
        /// <returns>Settings for a xml reader</returns>
        public XmlReaderSettings BuildXmlReaderSettings(bool isXmlValidationEnabled)
        {
            var settings = new XmlReaderSettings();

            if (isXmlValidationEnabled)
            {
                settings.ValidationEventHandler += ValidationCallback;
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(_xmlSchemaReader.GetXmlSchema(@"Data\Xsd\BitmapFont.xsd"));
            }
            else
            {
                settings.ValidationType = ValidationType.None;
            }

            return settings;
        }

        /// <summary>
        /// Handler for validation errors.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The arguments of the event</param>
        private static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            throw new FontLoaderException(args.Message);
        }
    }
}