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
using BitmapFontLibrary.Loader.Exception;
using BitmapFontLibrary.Loader.Texture;
using BitmapFontLibrary.Model;

namespace BitmapFontLibrary.Loader.Parser.Text
{
    /// <summary>
    /// Parser for the text Angelcode Bitmap Font format.
    /// </summary>
    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class TextFontFileParser : IFontFileParser
    {
        private readonly ITextReader _reader;
        private readonly IIntAdapter _intAdapter;
        private readonly IFontTextureLoader _fontTextureLoader;
        private Font _font;
        private string _imageDirectoryPath;

        /// <summary>
        /// Parser for the text Angelcode Bitmap Font format.
        /// </summary>
        /// <param name="textReader">Object of a class that implements the ITextReader interface</param>
        /// <param name="intAdapter">Object of a class that implements the IIntAdapter interface</param>
        /// <param name="fontTextureLoader">Object of a class that implements the IFontTextureLoader interface</param>
        public TextFontFileParser(ITextReader textReader, IIntAdapter intAdapter, IFontTextureLoader fontTextureLoader)
        {
            if (textReader == null) throw new ArgumentNullException("textReader");
            if (intAdapter == null) throw new ArgumentNullException("intAdapter");
            if (fontTextureLoader == null) throw new ArgumentNullException("fontTextureLoader");
            _reader = textReader;
            _intAdapter = intAdapter;
            _fontTextureLoader = fontTextureLoader;
        }

        /// <summary>
        /// Parses text Angelcode Bitmap Font formats.
        /// </summary>
        /// <param name="inputStream">Stream which contains the input to parse</param>
        /// <param name="imageDirectoryPath">Path to the directory that contains the bitmap images</param>
        /// <returns>The parsed font</returns>
        public Font Parse(Stream inputStream, string imageDirectoryPath)
        {
            _font = new Font();
            _reader.Initialize(inputStream);
            _imageDirectoryPath = imageDirectoryPath;

            ParseTags();
            _reader.Close();

            return _font;
        }

        /// <summary>
        /// Parses the different tags.
        /// </summary>
        private void ParseTags()
        {
            while (_reader.ElementType == TextElementType.Tag)
            {
                if (!_reader.ReadTag()) break;

                switch (_reader.Name)
                {
                    case "info":
                        ParseInfoAttributes();
                        break;
                    case "common":
                        ParseCommonAttributes();
                        break;
                    case "page":
                        ParsePageAttributes();
                        break;
                    case "char":
                        ParseCharAttributes();
                        break;
                    case "kerning":
                        ParseKerningAttributes();
                        break;
                    default:
                        _reader.MoveToNextTag();
                        break;
                }
            }
        }

        /// <summary>
        /// Parses the attributes of an info tag.
        /// </summary>
        private void ParseInfoAttributes()
        {
            while (_reader.ElementType == TextElementType.Attribute)
            {
                if (!_reader.ReadAttribute()) break;

                switch (_reader.Name)
                {
                    case "face":
                        _font.Name = _reader.ReadValueAsString();
                        break;
                    case "size":
                        _font.Size = _reader.ReadValueAsInteger();
                        break;
                    case "bold":
                        _font.IsBold = _reader.ReadValueAsBoolean();
                        break;
                    case "italic":
                        _font.IsItalic = _reader.ReadValueAsBoolean();
                        break;
                    case "charset":
                        _font.Charset = _reader.ReadValueAsString();
                        break;
                    case "unicode":
                        _font.IsUnicode = _reader.ReadValueAsBoolean();
                        break;
                    case "stretchH":
                        _font.StretchHeight = _reader.ReadValueAsInteger();
                        break;
                    case "smooth":
                        _font.IsSmooth = _reader.ReadValueAsBoolean();
                        break;
                    case "aa":
                        _font.SuperSamplingLevel = _reader.ReadValueAsInteger();
                        break;
                    case "padding":
                        var paddings = _reader.ReadValueAsCommaSeparatedIntegers();
                        if (paddings.Length != 4) throw new FontLoaderException("Invalid padding values");
                        _font.PaddingTop = paddings[0];
                        _font.PaddingRight = paddings[1];
                        _font.PaddingBottom = paddings[2];
                        _font.PaddingLeft = paddings[3];
                        break;
                    case "spacing":
                        var spacings = _reader.ReadValueAsCommaSeparatedIntegers();
                        if (spacings.Length != 2) throw new FontLoaderException("Invalid spacing values");
                        _font.SpacingTop = spacings[0];
                        _font.SpacingLeft = spacings[1];
                        break;
                    case "outline":
                        _font.OutlineThickness = _reader.ReadValueAsInteger();
                        break;
                }
            }
        }

        /// <summary>
        /// Parses the attributes of a common tag.
        /// </summary>
        private void ParseCommonAttributes()
        {
            while (_reader.ElementType == TextElementType.Attribute)
            {
                _reader.ReadAttribute();
                switch (_reader.Name)
                {
                    case "lineHeight":
                        _font.LineHeight = _reader.ReadValueAsInteger();
                        break;
                    case "base":
                        _font.BaseHeight = _reader.ReadValueAsInteger();
                        break;
                    case "scaleW":
                        _font.ScaleWidth = _reader.ReadValueAsInteger();
                        break;
                    case "scaleH":
                        _font.ScaleHeight = _reader.ReadValueAsInteger();
                        break;
                    case "pages":
                        _font.PagesCount = _reader.ReadValueAsInteger();
                        break;
                    case "packed":
                        _font.AreCharactersPackedInMultipleChannels = _reader.ReadValueAsBoolean();
                        break;
                    case "alphaChnl":
                        _font.AlphaChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadValueAsInteger());
                        break;
                    case "redChnl":
                        _font.RedChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadValueAsInteger());
                        break;
                    case "greenChnl":
                        _font.GreenChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadValueAsInteger());
                        break;
                    case "blueChnl":
                        _font.BlueChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadValueAsInteger());
                        break;
                }
            }
        }

        /// <summary>
        /// Parses the attributes of a page tag.
        /// </summary>
        private void ParsePageAttributes()
        {
            var pageId = -1;
            IFontTexture fontTexture = null;

            while (_reader.ElementType == TextElementType.Attribute)
            {
                _reader.ReadAttribute();
                switch (_reader.Name)
                {
                    case "id":
                        pageId = _reader.ReadValueAsInteger();
                        break;
                    case "file":
                        fontTexture = _fontTextureLoader.Load(Path.Combine(_imageDirectoryPath, _reader.ReadValueAsString()), _font.IsSmooth);
                        break;
                }
            }

            if (pageId == -1 || fontTexture == null) throw new FontLoaderException("Invalid page values");
            _font.AddPage(pageId, fontTexture);
        }

        /// <summary>
        /// Parses the attributes of a char tag.
        /// </summary>
        private void ParseCharAttributes()
        {
            var character = new Character();
            var characterId = -1;

            while (_reader.ElementType == TextElementType.Attribute)
            {
                _reader.ReadAttribute();
                switch (_reader.Name)
                {
                    case "id":
                        characterId = _reader.ReadValueAsInteger();
                        break;
                    case "x":
                        character.X = _reader.ReadValueAsInteger();
                        break;
                    case "y":
                        character.Y = _reader.ReadValueAsInteger();
                        break;
                    case "width":
                        character.Width = _reader.ReadValueAsInteger();
                        break;
                    case "height":
                        character.Height = _reader.ReadValueAsInteger();
                        break;
                    case "xoffset":
                        character.XOffset = _reader.ReadValueAsInteger();
                        break;
                    case "yoffset":
                        character.YOffset = _reader.ReadValueAsInteger();
                        break;
                    case "xadvance":
                        character.XAdvance = _reader.ReadValueAsInteger();
                        break;
                    case "page":
                        character.Page = _reader.ReadValueAsInteger();
                        break;
                    case "chnl":
                        character.Channel = _intAdapter.IntToEnum<Channel>(_reader.ReadValueAsInteger());
                        break;
                }
            }

            _font.AddCharacter(characterId, character);
        }

        /// <summary>
        /// Parses the attributes of a kerning attributes tag.
        /// </summary>
        private void ParseKerningAttributes()
        {
            var firstCharacterId = -1;
            var secondCharacterId = -1;
            var amount = 0;

            while (_reader.ElementType == TextElementType.Attribute)
            {
                _reader.ReadAttribute();
                switch (_reader.Name)
                {
                    case "first":
                        firstCharacterId = _reader.ReadValueAsInteger();
                        break;
                    case "second":
                        secondCharacterId = _reader.ReadValueAsInteger();
                        break;
                    case "amount":
                        amount = _reader.ReadValueAsInteger();
                        break;
                }
            }

            if (firstCharacterId == -1 || secondCharacterId == -1) throw new FontLoaderException("Invalid kerning values");
            _font.AddKerning(firstCharacterId, secondCharacterId, amount);
        }
    }
}