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
    /// Interface for the configuration of a text.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface ITextConfiguration
    {
        /// <summary>
        /// The size of the font in em.
        /// </summary>
        float SizeInEms { get; set; }

        /// <summary>
        /// The size of the font in pt.
        /// </summary>
        uint SizeInPoints { get; set; }

        /// <summary>
        /// The size of the font in px.
        /// </summary>
        uint SizeInPixels { get; set; }

        /// <summary>
        /// The size of the font in %.
        /// </summary>
        uint SizeInPercent { get; set; }

        /// <summary>
        /// The line spacing factor.
        /// </summary>
        float LineSpacing { get; set; }

        /// <summary>
        /// The alignment of the text.
        /// </summary>
        TextAlignment Alignment { get; set; }

        /// <summary>
        /// The maximal width of the text.
        /// </summary>
        float MaximalWidth { get; set; }

        /// <summary>
        /// The maximal height of the text.
        /// </summary>
        float MaximalHeight { get; set; }
    }
}