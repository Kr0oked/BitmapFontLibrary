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
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Loader.Exception;
using BitmapFontLibrary.Loader.Texture;
using BitmapFontLibrary.Model;

namespace BitmapFontLibrary.Loader.Parser.Binary
{
    /// <summary>
    /// Parser for the binary Angelcode Bitmap Font format.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class BinaryFontFileParser : IFontFileParser
    {
        private readonly IIntAdapter _intAdapter;
        private readonly IFontTextureLoader _fontTextureLoader;
        private Font _font;
        private BinaryReader _reader;
        private string _imageDirectoryPath;

        /// <summary>
        /// Parser for the binary Angelcode Bitmap Font format.
        /// </summary>
        /// <param name="intAdapter">Object of a class that implements the IIntAdapter interface</param>
        /// <param name="fontTextureLoader">Object of a class that implements the IFontTextureLoader interface</param>
        public BinaryFontFileParser(IIntAdapter intAdapter, IFontTextureLoader fontTextureLoader)
        {
            if (intAdapter == null) throw new ArgumentNullException("intAdapter");
            if (fontTextureLoader == null) throw new ArgumentNullException("fontTextureLoader");
            _intAdapter = intAdapter;
            _fontTextureLoader = fontTextureLoader;
        }

        /// <summary>
        /// Parses binary Angelcode Bitmap Font formats.
        /// </summary>
        /// <param name="inputStream">Stream which contains the input to parse</param>
        /// <param name="imageDirectoryPath">Path to the directory that contains the bitmap images</param>
        /// <returns>The parsed font</returns>
        public Font Parse(Stream inputStream, string imageDirectoryPath)
        {
            _font = new Font();

            using (_reader = new BinaryReader(inputStream))
            {
                _imageDirectoryPath = imageDirectoryPath;

                ParseVersion();

                var isEndOfStream = false;
                while (!isEndOfStream)
                {
                    try
                    {
                        var blockTypeIdentifier = _reader.ReadByte();
                        var blockSize = _reader.ReadInt32();

                        switch (blockTypeIdentifier)
                        {
                            case 1:
                                ParseInfoBlock();
                                break;
                            case 2:
                                ParseCommonBlock();
                                break;
                            case 3:
                                ParsePagesBlock();
                                break;
                            case 4:
                                ParseCharsBlock(blockSize);
                                break;
                            case 5:
                                ParseKerningPairsBlock(blockSize);
                                break;
                            default:
                                _reader.ReadBytes(blockSize);
                                break;
                        }
                    }
                    catch (EndOfStreamException)
                    {
                        isEndOfStream = true;
                    }
                }
            }

            return _font;
        }

        /// <summary>
        /// Parses the version
        /// </summary>
        private void ParseVersion()
        {
            var identifier = "";
            for (var i = 0; i < 3; i++)
            {
                identifier += _reader.ReadChar();    
            }

            var version = _reader.ReadByte();

            if (identifier != "BMF") throw new FontLoaderException("Wrong identifier in BMF file");
            if (version != 3) throw new FontLoaderException("Unsupported BMF file version");
        }

        /// <summary>
        /// Parses the info block
        /// </summary>
        private void ParseInfoBlock()
        {
            _font.Size = _reader.ReadInt16();

            var bitField = new BitArray(new[]{_reader.ReadByte()});
            _font.IsBold = bitField.Get(4);
            _font.IsItalic = bitField.Get(5);
            _font.IsUnicode = bitField.Get(6);
            _font.IsSmooth = bitField.Get(7);

            _font.Charset = Convert.ToString(Convert.ToInt32(_reader.ReadByte()));
            _font.StretchHeight = _reader.ReadUInt16();
            _font.SuperSamplingLevel = _reader.ReadByte();
            _font.PaddingTop = _reader.ReadByte();
            _font.PaddingRight = _reader.ReadByte();
            _font.PaddingBottom = _reader.ReadByte();
            _font.PaddingLeft = _reader.ReadByte();
            _font.SpacingLeft = _reader.ReadByte();
            _font.SpacingTop = _reader.ReadByte();
            _font.OutlineThickness = _reader.ReadByte();

            var fontName = "";
            char character;
            while ((character = _reader.ReadChar()) != 0)
            {
                fontName += character;
            }
            _font.Name = fontName;
        }

        /// <summary>
        /// Parses the common block
        /// </summary>
        private void ParseCommonBlock()
        {
            _font.LineHeight = _reader.ReadUInt16();
            _font.BaseHeight = _reader.ReadUInt16();
            _font.ScaleWidth = _reader.ReadUInt16();
            _font.ScaleHeight = _reader.ReadUInt16();
            _font.PagesCount = _reader.ReadUInt16();

            var bitField = new BitArray(new []{_reader.ReadByte()});
            _font.AreCharactersPackedInMultipleChannels = bitField.Get(0);

            _font.AlphaChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadByte());
            _font.RedChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadByte());
            _font.GreenChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadByte());
            _font.BlueChannel = _intAdapter.IntToEnum<ChannelValue>(_reader.ReadByte());
        }

        /// <summary>
        /// Parses the pages block
        /// </summary>
        private void ParsePagesBlock()
        {
            for (var i = 0; i < _font.PagesCount; i++)
            {
                var pageName = "";
                char character;
                while ((character = _reader.ReadChar()) != 0)
                {
                    pageName += character;
                }
                var fontTexture = _fontTextureLoader.Load(Path.Combine(_imageDirectoryPath, pageName), _font.IsSmooth);
                _font.AddPage(i, fontTexture);
            }
        }

        /// <summary>
        /// Parses the chars block
        /// </summary>
        /// <param name="blockSize">Block size in bytes</param>
        private void ParseCharsBlock(int blockSize)
        {
            var charactersCount = blockSize/20;

            for (var i = 0; i < charactersCount; i++)
            {
                var characterId = _reader.ReadInt32();

                var character = new Character
                {
                    X = _reader.ReadUInt16(),
                    Y = _reader.ReadUInt16(),
                    Width = _reader.ReadUInt16(),
                    Height = _reader.ReadUInt16(),
                    XOffset = _reader.ReadInt16(),
                    YOffset = _reader.ReadInt16(),
                    XAdvance = _reader.ReadInt16(),
                    Page = _reader.ReadByte(),
                    Channel = _intAdapter.IntToEnum<Channel>(_reader.ReadByte())
                };

                _font.AddCharacter(characterId, character);
            }
        }

        /// <summary>
        /// Parses the kerning pairs block
        /// </summary>
        /// <param name="blockSize">Block size in bytes</param>
        private void ParseKerningPairsBlock(int blockSize)
        {
            var kerningPairsCount = blockSize/10;

            for (var i = 0; i < kerningPairsCount; i++)
            {
                var firstCharacterId = _reader.ReadInt32();
                var secondCharacterId = _reader.ReadInt32();
                var amount = _reader.ReadInt16();

                _font.AddKerning(firstCharacterId, secondCharacterId, amount);
            }
        }
    }
}