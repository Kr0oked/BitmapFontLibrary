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
using System.Xml;
using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Loader.Exception;
using BitmapFontLibrary.Loader.Texture;
using BitmapFontLibrary.Model;

namespace BitmapFontLibrary.Loader.Parser.Xml
{
    /// <summary>
    /// Parser for the xml Angelcode Bitmap Font format.
    /// </summary>
    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class XmlFontFileParser : IFontFileParser
    {
        private readonly IXmlSettingsBuilder _xmlSettingsBuilder;
        private readonly IStringAdapter _stringAdapter;
        private readonly IIntAdapter _intAdapter;
        private readonly IFontTextureLoader _fontTextureLoader;
        private IFont _font;
        private XmlReader _reader;
        private string _imageDirectoryPath;

        /// <summary>
        /// Activates or deactivates the validation of the xml.
        /// </summary>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public bool IsXmlValidatingEnabled { get; set; }

        /// <summary>
        /// Parser for the xml Angelcode Bitmap Font format.
        /// </summary>
        /// <param name="xmlSettingsBuilder">Object of a class that implements the IXmlSettingsBuilder interface</param>
        /// <param name="stringAdapter">Object of a class that implements the IStringAdapter interface</param>
        /// <param name="intAdapter">Object of a class that implements the IIntAdapter interface</param>
        /// <param name="fontTextureLoader">Object of a class that implements the IFontTextureLoader interface</param>
        public XmlFontFileParser(IXmlSettingsBuilder xmlSettingsBuilder, IStringAdapter stringAdapter,
            IIntAdapter intAdapter, IFontTextureLoader fontTextureLoader)
        {
            if (xmlSettingsBuilder == null) throw new ArgumentNullException("xmlSettingsBuilder");
            if (stringAdapter == null) throw new ArgumentNullException("stringAdapter");
            if (intAdapter == null) throw new ArgumentNullException("intAdapter");
            if (fontTextureLoader == null) throw new ArgumentNullException("fontTextureLoader");
            _xmlSettingsBuilder = xmlSettingsBuilder;
            _stringAdapter = stringAdapter;
            _intAdapter = intAdapter;
            _fontTextureLoader = fontTextureLoader;
            IsXmlValidatingEnabled = true;
        }

        /// <summary>
        /// Parses binary Angelcode Bitmap Font formats.
        /// </summary>
        /// <param name="inputStream">Stream which contains the input to parse</param>
        /// <param name="imageDirectoryPath">Path to the directory that contains the bitmap images</param>
        /// <returns>The parsed font</returns>
        public IFont Parse(Stream inputStream, string imageDirectoryPath)
        {
            _font = new Font();
            _imageDirectoryPath = imageDirectoryPath;

            using (_reader = XmlReader.Create(inputStream, _xmlSettingsBuilder.BuildXmlReaderSettings(IsXmlValidatingEnabled)))
            {
                _reader.MoveToContent();
                ParseRootElement();
            }

            return _font;
        }

        /// <summary>
        /// Parses the root element.
        /// </summary>
        private void ParseRootElement()
        {
            while (_reader.Read())
            {
                if (_reader.NodeType != XmlNodeType.Element) continue;
                switch (_reader.Name)
                {
                    case "info":
                        ParseInfoElement();
                        break;
                    case "common":
                        ParseCommonElement();
                        break;
                    case "page":
                        ParsePageElement();
                        break;
                    case "char":
                        ParseCharElement();
                        break;
                    case "kerning":
                        ParseKerningElement();
                        break;
                }
            }
        }

        /// <summary>
        /// Parses an info element.
        /// </summary>
        private void ParseInfoElement()
        {
            while (_reader.MoveToNextAttribute())
            {
                switch (_reader.Name)
                {
                    case "face":
                        _font.Name = _reader.ReadContentAsString();
                        break;
                    case "size":
                        _font.Size = _reader.ReadContentAsInt();
                        break;
                    case "bold":
                        _font.IsBold = Convert.ToBoolean(_reader.ReadContentAsInt());
                        break;
                    case "italic":
                        _font.IsItalic = Convert.ToBoolean(_reader.ReadContentAsInt());
                        break;
                    case "charset":
                        _font.Charset = _reader.ReadContentAsString();
                        break;
                    case "unicode":
                        _font.IsUnicode = Convert.ToBoolean(_reader.ReadContentAsInt());
                        break;
                    case "stretchH":
                        _font.StretchHeight = _reader.ReadContentAsInt();
                        break;
                    case "smooth":
                        _font.IsSmooth = Convert.ToBoolean(_reader.ReadContentAsInt());
                        break;
                    case "aa":
                        _font.SuperSamplingLevel = _reader.ReadContentAsInt();
                        break;
                    case "padding":
                        var paddings = _stringAdapter.CommaSeparatedIntStringToIntArray(_reader.ReadContentAsString());
                        if (paddings.Length != 4) throw new FontLoaderException("Invalid padding values");
                        _font.PaddingTop = paddings[0];
                        _font.PaddingRight = paddings[1];
                        _font.PaddingBottom = paddings[2];
                        _font.PaddingLeft = paddings[3];
                        break;
                    case "spacing":
                        var spacings = _stringAdapter.CommaSeparatedIntStringToIntArray(_reader.ReadContentAsString());
                        if (spacings.Length != 2) throw new FontLoaderException("Invalid spacing values");
                        _font.SpacingTop = spacings[0];
                        _font.SpacingLeft = spacings[1];
                        break;
                    case "outline":
                        _font.OutlineThickness = _reader.ReadContentAsInt();
                        break;
                }
            }
        }

        /// <summary>
        /// Parses a common element.
        /// </summary>
        private void ParseCommonElement()
        {
            while (_reader.MoveToNextAttribute())
            {
                switch (_reader.Name)
                {
                    case "lineHeight":
                        _font.LineHeight = _reader.ReadContentAsInt();
                        break;
                    case "base":
                        _font.BaseHeight = _reader.ReadContentAsInt();
                        break;
                    case "scaleW":
                        _font.ScaleWidth = _reader.ReadContentAsInt();
                        break;
                    case "scaleH":
                        _font.ScaleHeight = _reader.ReadContentAsInt();
                        break;
                    case "pages":
                        _font.PagesCount = _reader.ReadContentAsInt();
                        break;
                    case "packed":
                        _font.AreCharactersPackedInMultipleChannels = Convert.ToBoolean(_reader.ReadContentAsInt());
                        break;
                    case "alphaChnl":
                        _font.AlphaChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadContentAsInt());
                        break;
                    case "redChnl":
                        _font.RedChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadContentAsInt());
                        break;
                    case "greenChnl":
                        _font.GreenChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadContentAsInt());
                        break;
                    case "blueChnl":
                        _font.BlueChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadContentAsInt());
                        break;
                }
            }
        }

        /// <summary>
        /// Parses a page element.
        /// </summary>
        private void ParsePageElement()
        {
            var pageId = -1;
            IFontTexture fontTexture = null;

            while (_reader.MoveToNextAttribute())
            {
                switch (_reader.Name)
                {
                    case "id":
                        pageId = _reader.ReadContentAsInt();
                        break;
                    case "file":
                        fontTexture = _fontTextureLoader.Load(Path.Combine(_imageDirectoryPath, _reader.ReadContentAsString()), _font.IsSmooth);
                        break;
                }
            }

            if (pageId == -1 || fontTexture == null) throw new FontLoaderException("Invalid page values");
            _font.AddPage(pageId, fontTexture);
        }

        /// <summary>
        /// Parses a char element.
        /// </summary>
        private void ParseCharElement()
        {
            var character = new Character();
            var characterId = -1;

            while (_reader.MoveToNextAttribute())
            {
                switch (_reader.Name)
                {
                    case "id":
                        characterId = _reader.ReadContentAsInt();
                        break;
                    case "x":
                        character.X = _reader.ReadContentAsInt();
                        break;
                    case "y":
                        character.Y = _reader.ReadContentAsInt();
                        break;
                    case "width":
                        character.Width = _reader.ReadContentAsInt();
                        break;
                    case "height":
                        character.Height = _reader.ReadContentAsInt();
                        break;
                    case "xoffset":
                        character.XOffset = _reader.ReadContentAsInt();
                        break;
                    case "yoffset":
                        character.YOffset = _reader.ReadContentAsInt();
                        break;
                    case "xadvance":
                        character.XAdvance = _reader.ReadContentAsInt();
                        break;
                    case "page":
                        character.Page = _reader.ReadContentAsInt();
                        break;
                    case "chnl":
                        character.Channel = _intAdapter.IntToEnum<Channel>(_reader.ReadContentAsInt());
                        break;
                }
            }

            _font.AddCharacter(characterId, character);
        }

        /// <summary>
        /// Parses a kerning element.
        /// </summary>
        private void ParseKerningElement()
        {
            var firstCharacterId = -1;
            var secondCharacterId = -1;
            var amount = 0;

            while (_reader.MoveToNextAttribute())
            {
                switch (_reader.Name)
                {
                    case "first":
                        firstCharacterId = _reader.ReadContentAsInt();
                        break;
                    case "second":
                        secondCharacterId = _reader.ReadContentAsInt();
                        break;
                    case "amount":
                        amount = _reader.ReadContentAsInt();
                        break;
                }
            }

            if (firstCharacterId == -1 || secondCharacterId == -1) throw new FontLoaderException("Invalid kerning values");
            _font.AddKerning(firstCharacterId, secondCharacterId, amount);
        }
    }
}