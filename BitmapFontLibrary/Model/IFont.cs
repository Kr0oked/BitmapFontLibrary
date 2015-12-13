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

namespace BitmapFontLibrary.Model
{
    /// <summary>
    /// Interface for Informations about a font.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface IFont
    {
        /// <summary>
        /// The name of the font.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The size of the font in px.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Is the font bold.
        /// </summary>
        bool IsBold { get; set; }

        /// <summary>
        /// Is the font italic.
        /// </summary>
        bool IsItalic { get; set; }

        /// <summary>
        /// Name of the OEM charset used when the font does not use the unicode charset.
        /// </summary>
        string Charset { get; set; }

        /// <summary>
        /// Is the charset unicode.
        /// </summary>
        bool IsUnicode { get; set; }

        /// <summary>
        /// The font height stretch in percentage. 100% means no stretch.
        /// </summary>
        int StretchHeight { get; set; }

        /// <summary>
        /// True if smoothing was turned on.
        /// </summary>
        bool IsSmooth { get; set; }

        /// <summary>
        /// The supersampling level used. 1 means no supersampling was used.
        /// </summary>
        int SuperSamplingLevel { get; set; }

        /// <summary>
        /// The top padding for each character.
        /// </summary>
        int PaddingTop { get; set; }

        /// <summary>
        /// The right padding for each character.
        /// </summary>
        int PaddingRight { get; set; }

        /// <summary>
        /// The bottom padding for each character.
        /// </summary>
        int PaddingBottom { get; set; }

        /// <summary>
        /// The left padding for each character.
        /// </summary>
        int PaddingLeft { get; set; }

        /// <summary>
        /// The top spacing for each character.
        /// </summary>
        int SpacingTop { get; set; }

        /// <summary>
        /// The left spacing for each character.
        /// </summary>
        int SpacingLeft { get; set; }

        /// <summary>
        /// The outline thickness for the characters.
        /// </summary>
        int OutlineThickness { get; set; }

        /// <summary>
        /// The distance in pixels between each line of text.
        /// </summary>
        int LineHeight { get; set; }

        /// <summary>
        /// The number of pixels from the absolute top of the line to the base of the characters.
        /// </summary>
        int BaseHeight { get; set; }

        /// <summary>
        /// The width of the texture, normally used to scale the x pos of the character image.
        /// </summary>
        int ScaleWidth { get; set; }

        /// <summary>
        /// The height of the texture, normally used to scale the y pos of the character image.
        /// </summary>
        int ScaleHeight { get; set; }

        /// <summary>
        /// The number of texture pages included in the font.
        /// </summary>
        int PagesCount { get; set; }

        /// <summary>
        /// True if monochrome characters have been packed into each of the texture channels.
        /// In this case the alpha channel describes what is stored in each channel.
        /// </summary>
        bool AreCharactersPackedInMultipleChannels { get; set; }

        /// <summary>
        /// Value in the alpha channel.
        /// </summary>
        ChannelValue AlphaChannel { get; set; }

        /// <summary>
        /// Value in the red channel.
        /// </summary>
        ChannelValue RedChannel { get; set; }

        /// <summary>
        /// Value in the green channel.
        /// </summary>
        ChannelValue GreenChannel { get; set; }

        /// <summary>
        /// Value in the blue channel.
        /// </summary>
        ChannelValue BlueChannel { get; set; }

        /// <summary>
        /// Gets the page with the specified id.
        /// </summary>
        /// <param name="pageId">The id of the page</param>
        /// <returns>The page</returns>
        IFontTexture GetPage(int pageId);

        /// <summary>
        /// Adds a page.
        /// </summary>
        /// <param name="pageId">The id of the page</param>
        /// <param name="page">The page to add</param>
        void AddPage(int pageId, IFontTexture page);

        /// <summary>
        /// Gets a character by its id.
        /// </summary>
        /// <param name="characterId">The id of the character</param>
        /// <returns>The character</returns>
        ICharacter GetCharacterById(int characterId);

        /// <summary>
        /// Gets a character by its index.
        /// </summary>
        /// <param name="characterIndex">The index of the character</param>
        /// <returns>The character</returns>
        ICharacter GetCharacterByIndex(int characterIndex);

        /// <summary>
        /// Adds a character.
        /// </summary>
        /// <param name="characterId">The id of the character</param>
        /// <param name="character">The character to add</param>
        void AddCharacter(int characterId, ICharacter character);

        /// <summary>
        /// Gets the index of a character by its id.
        /// </summary>
        /// <param name="characterId">The id of the character.</param>
        /// <returns>The index of the character</returns>
        int GetCharacterIndex(int characterId);

        /// <summary>
        /// Gets the size of the characters.
        /// </summary>
        /// <returns>The size of the characters</returns>
        int GetCharactersSize();

        /// <summary>
        /// Gets the kerning amount between two characters.
        /// </summary>
        /// <param name="firstCharacterId">The id of the first character</param>
        /// <param name="secondCharacterId">The id of the second character</param>
        /// <returns>The kerning amount between the two characters</returns>
        int GetKerningAmount(int firstCharacterId, int secondCharacterId);

        /// <summary>
        /// Adds a kerning amount between two characters.
        /// </summary>
        /// <param name="firstCharacterId">The id of the first character</param>
        /// <param name="secondCharacterId">The id of the second character</param>
        /// <param name="amount">The kerning amount between the two characters</param>
        void AddKerning(int firstCharacterId, int secondCharacterId, int amount);
    }
}