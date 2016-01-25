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

using BitmapFontLibrary.Model;

namespace BitmapFontLibrary
{
    /// <summary>
    /// Draws texts with bitmap fonts.
    /// </summary>
    public interface IBitmapFont
    {
        /// <summary>
        /// Initializes the bitmap font.
        /// </summary>
        /// <param name="path">Path to a Angelcode Bitmap Font file</param>
        void Initialize(string path);

        /// <summary>
        /// Draws a text.
        /// </summary>
        /// <param name="text">The text to draw</param>
        /// <param name="x">The x-coordinate to start the text</param>
        /// <param name="y">The y-coordinate to start the text</param>
        /// <param name="z">The z-coordinate to start the text</param>
        /// <param name="textConfiguration">The configuration of the text</param>
        void Draw(string text, float x, float y, float z, ITextConfiguration textConfiguration);

        /// <summary>
        /// Draws a text with the standard configuration.
        /// </summary>
        /// <param name="text">The text to draw</param>
        /// <param name="x">The x-coordinate to start the text</param>
        /// <param name="y">The y-coordinate to start the text</param>
        /// <param name="z">The z-coordinate to start the text</param>
        void Draw(string text, float x, float y, float z);
    }
}