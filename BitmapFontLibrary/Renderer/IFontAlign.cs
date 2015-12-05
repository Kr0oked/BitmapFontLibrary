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

namespace BitmapFontLibrary.Renderer
{
    /// <summary>
    /// Interface for aligning text.
    /// </summary>
    public interface IFontAlign
    {
        /// <summary>
        /// Starts aligning a new text.
        /// </summary>
        /// <param name="x">The x-coordinate where to start the text</param>
        /// <param name="y">The y-coordinate where to start the text</param>
        /// <param name="z">The z-coordinate where to start the text</param>
        /// <param name="size">The size of the text</param>
        void StartText(float x, float y, float z, float size);

        /// <summary>
        /// Add a kerning at the current position.
        /// </summary>
        /// <param name="kerningAmount">The kerning amount</param>
        void Kerning(int kerningAmount);

        /// <summary>
        /// Starts a new line.
        /// </summary>
        /// <param name="lineHeight">The height of a line</param>
        void NewLine(int lineHeight);

        /// <summary>
        /// Ends the text.
        /// </summary>
        void EndText();
    }
}