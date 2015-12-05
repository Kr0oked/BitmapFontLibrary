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
using System.Linq;
using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Model;

namespace BitmapFontLibrary.Renderer
{
    /// <summary>
    /// Renders text with a font.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class FontRenderer : IFontRenderer
    {
        private readonly ICharacterSprites _characterSprites;
        private readonly IFontAlign _fontAlign;
        private readonly ICharAdapter _charAdapter;
        private Font _font;

        /// <summary>
        /// Renders text with a font.
        /// </summary>
        /// <param name="characterSprites">Object of a class that implements the ICharacterSprites interface</param>
        /// <param name="fontAlign">Object of a class that implements the IFontAlign interface</param>
        /// <param name="charAdapter">Object of a class that implements the ICharAdapter interface</param>
        public FontRenderer(ICharacterSprites characterSprites, IFontAlign fontAlign, ICharAdapter charAdapter)
        {
            if (characterSprites == null) throw new ArgumentNullException("characterSprites");
            if (fontAlign == null) throw new ArgumentNullException("fontAlign");
            if (charAdapter == null) throw new ArgumentNullException("charAdapter");
            _characterSprites = characterSprites;
            _fontAlign = fontAlign;
            _charAdapter = charAdapter;
        }

        /// <summary>
        /// Initializes the renderer.
        /// </summary>
        /// <param name="font">The font to render with</param>
        public void Initialize(Font font)
        {
            if (font == null) throw new ArgumentNullException("font");
            if (!font.IsUnicode) throw new NotSupportedException("Unsupported Charset: " + font.Charset);
            if (font.AreCharactersPackedInMultipleChannels) throw new NotSupportedException("Characters packed in multiple channels are not supported");

            _font = font;
            var charactersSize = font.GetCharactersSize();

            _characterSprites.Initialize(charactersSize);
            for (var characterIndex = 0; characterIndex < charactersSize; characterIndex++)
            {
                var character = font.GetCharacterByIndex(characterIndex);
                _characterSprites.AddCharacterSprite(characterIndex, character);
            }
        }

        /// <summary>
        /// Renders a text.
        /// </summary>
        /// <param name="text">The text to render</param>
        /// <param name="x">The x-coordinate to start render</param>
        /// <param name="y">The y-coordinate to start render</param>
        /// <param name="z">The z-coordinate to start render</param>
        /// <param name="size">The size of the text</param>
        public void Render(string text, float x, float y, float z, float size)
        {
            if (_font == null) throw new FieldAccessException("Font is not initialized");
            if (string.IsNullOrEmpty(text)) return;

            var previousCharacterId = -1;

            _fontAlign.StartText(x, y, z, size);

            foreach (var charValue in text.ToCharArray().Select(character => _charAdapter.CharToIntCharValue(character)))
            {
                if (previousCharacterId == 13 && charValue == 10)
                {
                    _fontAlign.NewLine(_font.LineHeight);
                }
                else
                {
                    var fontCharacter = _font.GetCharacterById(charValue);
                    if (fontCharacter != null)
                    {
                        var fontTexture = _font.GetPage(fontCharacter.Page);
                        if (fontTexture != null)
                        {
                            var kerningAmount = _font.GetKerningAmount(previousCharacterId, charValue);
                            _fontAlign.Kerning(kerningAmount);

                            fontTexture.BeginUse();
                            _characterSprites.RenderCharacterSprite(_font.GetCharacterIndex(charValue));
                            fontTexture.EndUse();
                        }
                    }
                }
                previousCharacterId = charValue;
            }

            _fontAlign.EndText();
        }
    }
}