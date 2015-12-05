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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BitmapFontLibrary.Model
{
    /// <summary>
    /// Informations about a font.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Font
    {
        /// <summary>
        /// The name of the font.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The size of the font.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Is the font bold.
        /// </summary>
        public bool IsBold { get; set; }

        /// <summary>
        /// Is the font italic.
        /// </summary>
        public bool IsItalic { get; set; }

        /// <summary>
        /// Name of the OEM charset used when the font does not use the unicode charset.
        /// </summary>
        public string Charset { get; set; }

        /// <summary>
        /// Is the charset unicode.
        /// </summary>
        public bool IsUnicode { get; set; }

        /// <summary>
        /// The font height stretch in percentage. 100% means no stretch.
        /// </summary>
        public int StretchHeight { get; set; }

        /// <summary>
        /// True if smoothing was turned on.
        /// </summary>
        public bool IsSmooth { get; set; }

        /// <summary>
        /// The supersampling level used. 1 means no supersampling was used.
        /// </summary>
        public int SuperSamplingLevel { get; set; }

        /// <summary>
        /// The top padding for each character.
        /// </summary>
        public int PaddingTop { get; set; }

        /// <summary>
        /// The right padding for each character.
        /// </summary>
        public int PaddingRight { get; set; }

        /// <summary>
        /// The bottom padding for each character.
        /// </summary>
        public int PaddingBottom { get; set; }

        /// <summary>
        /// The left padding for each character.
        /// </summary>
        public int PaddingLeft { get; set; }

        /// <summary>
        /// The top spacing for each character.
        /// </summary>
        public int SpacingTop { get; set; }

        /// <summary>
        /// The left spacing for each character.
        /// </summary>
        public int SpacingLeft { get; set; }

        /// <summary>
        /// The outline thickness for the characters.
        /// </summary>
        public int OutlineThickness { get; set; }

        /// <summary>
        /// The distance in pixels between each line of text.
        /// </summary>
        public int LineHeight { get; set; }

        /// <summary>
        /// The number of pixels from the absolute top of the line to the base of the characters.
        /// </summary>
        public int BaseHeight { get; set; }

        /// <summary>
        /// The width of the texture, normally used to scale the x pos of the character image.
        /// </summary>
        public int ScaleWidth { get; set; }

        /// <summary>
        /// The height of the texture, normally used to scale the y pos of the character image.
        /// </summary>
        public int ScaleHeight { get; set; }

        /// <summary>
        /// The number of texture pages included in the font.
        /// </summary>
        public int PagesCount { get; set; }

        /// <summary>
        /// True if monochrome characters have been packed into each of the texture channels.
        /// In this case the alpha channel describes what is stored in each channel.
        /// </summary>
        public bool AreCharactersPackedInMultipleChannels { get; set; }

        /// <summary>
        /// Value in the alpha channel.
        /// </summary>
        public ChannelValue AlphaChannel { get; set; }

        /// <summary>
        /// Value in the red channel.
        /// </summary>
        public ChannelValue RedChannel { get; set; }

        /// <summary>
        /// Value in the green channel.
        /// </summary>
        public ChannelValue GreenChannel { get; set; }

        /// <summary>
        /// Value in the blue channel.
        /// </summary>
        public ChannelValue BlueChannel { get; set; }

        private readonly Dictionary<int, IFontTexture> _pages;
        private readonly SortedList<int, Character> _characters;
        private readonly Dictionary<int, Dictionary<int, int>> _kerningAmounts;

        /// <summary>
        /// Informations about a font.
        /// </summary>
        public Font()
        {
            Name = "";
            Size = 0;
            Charset = "";
            IsUnicode = true;
            StretchHeight = 0;
            PaddingTop = 0;
            PaddingRight = 0;
            PaddingBottom = 0;
            PaddingLeft = 0;
            SpacingTop = 0;
            SpacingLeft = 0;
            LineHeight = 0;
            BaseHeight = 0;
            ScaleWidth = 0;
            ScaleHeight = 0;
            AreCharactersPackedInMultipleChannels = false;
            AlphaChannel = ChannelValue.Outline;
            RedChannel = ChannelValue.Glyph;
            GreenChannel = ChannelValue.Glyph;
            BlueChannel = ChannelValue.Glyph;

            _pages = new Dictionary<int, IFontTexture>();
            _characters = new SortedList<int, Character>();
            _kerningAmounts = new Dictionary<int, Dictionary<int, int>>();
        }

        /// <summary>
        /// Checks if another font is equal.
        /// </summary>
        /// <param name="other">The other font</param>
        /// <returns>true if the fonts are equal, otherwise false</returns>
        protected bool Equals(Font other)
        {
            var arePagesEqual = _pages.Keys.Count == other._pages.Keys.Count &&
                                 _pages.Keys.All(
                                     k => other._pages.ContainsKey(k) && 
                                         _pages[k].Equals(other._pages[k]));

            var areCharactersEqual = _characters.Keys.Count == other._characters.Keys.Count &&
                                     _characters.Keys.All(
                                         k => other._characters.ContainsKey(k) && 
                                             _characters[k].Equals(other._characters[k]));

            var areKerningAmountsEqual = _kerningAmounts.Keys.Count == other._kerningAmounts.Keys.Count &&
                                         _kerningAmounts.Keys.All(
                                             k => other._kerningAmounts.ContainsKey(k) &&
                                                  _kerningAmounts[k].Keys.Count == other._kerningAmounts[k].Keys.Count &&
                                                  _kerningAmounts[k].Keys.All(
                                                      t => other._kerningAmounts[k].ContainsKey(t) &&
                                                           _kerningAmounts[k][t] == other._kerningAmounts[k][t]));

            return arePagesEqual && areCharactersEqual && areKerningAmountsEqual &&
                string.Equals(Name, other.Name) && Size == other.Size && IsBold == other.IsBold && IsItalic == other.IsItalic && 
                string.Equals(Charset, other.Charset) && IsUnicode == other.IsUnicode && StretchHeight == other.StretchHeight && 
                IsSmooth == other.IsSmooth && SuperSamplingLevel == other.SuperSamplingLevel && PaddingTop == other.PaddingTop && 
                PaddingRight == other.PaddingRight && PaddingBottom == other.PaddingBottom && PaddingLeft == other.PaddingLeft && 
                SpacingTop == other.SpacingTop && SpacingLeft == other.SpacingLeft && OutlineThickness == other.OutlineThickness && 
                LineHeight == other.LineHeight && BaseHeight == other.BaseHeight && ScaleWidth == other.ScaleWidth && 
                ScaleHeight == other.ScaleHeight && PagesCount == other.PagesCount && 
                AreCharactersPackedInMultipleChannels == other.AreCharactersPackedInMultipleChannels && AlphaChannel == other.AlphaChannel && 
                RedChannel == other.RedChannel && GreenChannel == other.GreenChannel && BlueChannel == other.BlueChannel;
        }

        /// <summary>
        /// Checks if another object is equal.
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>true if the objects are equal, otherwise false</returns>
        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Font) obj);
        }

        /// <summary>
        /// Returns the hashcode of the object.
        /// </summary>
        /// <returns>The hashcode of the object</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_pages != null ? _pages.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_characters != null ? _characters.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_kerningAmounts != null ? _kerningAmounts.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Size;
                hashCode = (hashCode*397) ^ IsBold.GetHashCode();
                hashCode = (hashCode*397) ^ IsItalic.GetHashCode();
                hashCode = (hashCode*397) ^ (Charset != null ? Charset.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ IsUnicode.GetHashCode();
                hashCode = (hashCode*397) ^ StretchHeight;
                hashCode = (hashCode*397) ^ IsSmooth.GetHashCode();
                hashCode = (hashCode*397) ^ SuperSamplingLevel;
                hashCode = (hashCode*397) ^ PaddingTop;
                hashCode = (hashCode*397) ^ PaddingRight;
                hashCode = (hashCode*397) ^ PaddingBottom;
                hashCode = (hashCode*397) ^ PaddingLeft;
                hashCode = (hashCode*397) ^ SpacingTop;
                hashCode = (hashCode*397) ^ SpacingLeft;
                hashCode = (hashCode*397) ^ OutlineThickness;
                hashCode = (hashCode*397) ^ LineHeight;
                hashCode = (hashCode*397) ^ BaseHeight;
                hashCode = (hashCode*397) ^ ScaleWidth;
                hashCode = (hashCode*397) ^ ScaleHeight;
                hashCode = (hashCode*397) ^ PagesCount;
                hashCode = (hashCode*397) ^ AreCharactersPackedInMultipleChannels.GetHashCode();
                hashCode = (hashCode*397) ^ (int) AlphaChannel;
                hashCode = (hashCode*397) ^ (int) RedChannel;
                hashCode = (hashCode*397) ^ (int) GreenChannel;
                hashCode = (hashCode*397) ^ (int) BlueChannel;
                return hashCode;
            }
        }

        /// <summary>
        /// Gets the page with the specified id.
        /// </summary>
        /// <param name="pageId">The id of the page</param>
        /// <returns>The page</returns>
        public IFontTexture GetPage(int pageId)
        {
            return !_pages.ContainsKey(pageId) ? null : _pages[pageId];
        }

        /// <summary>
        /// Adds a page.
        /// </summary>
        /// <param name="pageId">The id of the page</param>
        /// <param name="page">The page to add</param>
        public void AddPage(int pageId, IFontTexture page)
        {
            _pages[pageId] = page;
        }

        /// <summary>
        /// Gets a character by its id.
        /// </summary>
        /// <param name="characterId">The id of the character</param>
        /// <returns>The character</returns>
        public Character GetCharacterById(int characterId)
        {
            return !_characters.ContainsKey(characterId) ? null : _characters[characterId];
        }

        /// <summary>
        /// Gets a character by its index.
        /// </summary>
        /// <param name="characterIndex">The index of the character</param>
        /// <returns>The character</returns>
        public Character GetCharacterByIndex(int characterIndex)
        {
            return _characters.Count > characterIndex ? _characters.Values[characterIndex] : null;
        }

        /// <summary>
        /// Adds a character.
        /// </summary>
        /// <param name="characterId">The id of the character</param>
        /// <param name="character">The character to add</param>
        public void AddCharacter(int characterId, Character character)
        {
            _characters.Add(characterId, character);
        }

        /// <summary>
        /// Gets the index of a character by its id.
        /// </summary>
        /// <param name="characterId">The id of the character.</param>
        /// <returns>The index of the character</returns>
        public int GetCharacterIndex(int characterId)
        {
            return _characters.IndexOfKey(characterId);
        }

        /// <summary>
        /// Gets the size of the characters.
        /// </summary>
        /// <returns>The size of the characters</returns>
        public int GetCharactersSize()
        {
            return _characters.Count;
        }

        /// <summary>
        /// Gets the kerning amount between two characters.
        /// </summary>
        /// <param name="firstCharacterId">The id of the first character</param>
        /// <param name="secondCharacterId">The id of the second character</param>
        /// <returns>The kerning amount between the two characters</returns>
        public int GetKerningAmount(int firstCharacterId, int secondCharacterId)
        {
            if (!_kerningAmounts.ContainsKey(firstCharacterId)) return 0;
            return _kerningAmounts[firstCharacterId].ContainsKey(secondCharacterId) ? _kerningAmounts[firstCharacterId][secondCharacterId] : 0;
        }

        /// <summary>
        /// Adds a kerning amount between two characters.
        /// </summary>
        /// <param name="firstCharacterId">The id of the first character</param>
        /// <param name="secondCharacterId">The id of the second character</param>
        /// <param name="amount">The kerning amount between the two characters</param>
        public void AddKerning(int firstCharacterId, int secondCharacterId, int amount)
        {
            if (!_kerningAmounts.ContainsKey(firstCharacterId))
            {
                _kerningAmounts[firstCharacterId] = new Dictionary<int, int>();
            }
            _kerningAmounts[firstCharacterId][secondCharacterId] = amount;
        }
    }
}