﻿#region License
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
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface ICharacter
    {
        /// <summary>
        /// The left position of the character in the texture.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// The top position of the character in the texture.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// The width of the character image in the texture.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// The height of the character image in the texture.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// How much the current position should be offset when copying the image from the texture to the screen.
        /// </summary>
        int XOffset { get; set; }

        /// <summary>
        /// How much the current position should be offset when copying the image from the texture to the screen.
        /// </summary>
        int YOffset { get; set; }

        /// <summary>
        /// How much the current position should be advanced after drawing the character.
        /// </summary>
        int XAdvance { get; set; }

        /// <summary>
        /// The texture page where the character image is found.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// The texture channel where the character image is found.
        /// </summary>
        Channel Channel { get; set; }
    }
}