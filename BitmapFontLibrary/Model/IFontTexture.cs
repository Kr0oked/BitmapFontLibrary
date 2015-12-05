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
using OpenTK.Graphics.OpenGL;

namespace BitmapFontLibrary.Model
{
    /// <summary>
    /// Interface for a texture which holds character images.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IFontTexture
    {
        /// <summary>
        /// The id of the texture.
        /// </summary>
        uint Id { get; }

        /// <summary>
        /// The texture width.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// The texture height.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// True if smoothing was turned on.
        /// </summary>
        bool IsSmooth { get; }

        /// <summary>
        /// Initializes a new font texture.
        /// </summary>
        /// <param name="pixels">Pointer to the pixels</param>
        /// <param name="width">Width of the texture</param>
        /// <param name="height">Height of the texture</param>
        /// <param name="isSmooth">True if smoothing was turned on</param>
        /// <param name="internalFormat">The internal pixel format</param>
        /// <param name="inputFormat">The input pixel format</param>
        void Initialize(IntPtr pixels, int width, int height, bool isSmooth, PixelInternalFormat internalFormat,
            PixelFormat inputFormat);

        /// <summary>
        /// Starts using the texture.
        /// </summary>
        void BeginUse();

        /// <summary>
        /// Ends using the texture.
        /// </summary>
        void EndUse();
    }
}