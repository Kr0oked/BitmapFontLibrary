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
    /// Texture which holds character images.
    /// </summary>
    public class FontTexture : IFontTexture, IDisposable
    {
        /// <summary>
        /// The id of the texture.
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        /// The texture width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The texture height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// True if smoothing was turned on.
        /// </summary>
        public bool IsSmooth { get; private set; }

        /// <summary>
        /// Returns true if these texture are supported by the system.
        /// </summary>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static bool IsSupported
        {
            get
            {
                return (new Version(GL.GetString(StringName.Version).Substring(0, 3)) >= new Version(3, 1));
            }
        }

        /// <summary>
        /// Texture which holds character images.
        /// </summary>
        public FontTexture()
        {
            if (!IsSupported) throw new NotSupportedException("Your system doesn't support Rectangle Textures");
            uint textureId;
            GL.GenTextures(1, out textureId);
            Id = textureId;
            Width = 0;
            Height = 0;
        }

        /// <summary>
        /// Disposes all used resources.
        /// </summary>
        public void Dispose()
        {
            if (Id != 0) GL.DeleteTexture(Id);
        }

        /// <summary>
        /// Initializes a new font texture.
        /// </summary>
        /// <param name="pixels">Pointer to the pixels</param>
        /// <param name="width">Width of the texture</param>
        /// <param name="height">Height of the texture</param>
        /// <param name="isSmooth">True if smoothing was turned on</param>
        /// <param name="internalFormat">The internal pixel format</param>
        /// <param name="inputFormat">The input pixel format</param>
        public void Initialize(IntPtr pixels, int width, int height, bool isSmooth, PixelInternalFormat internalFormat, PixelFormat inputFormat)
        {
            int textureMagFilter;
            int textureMinFilter;

            if (isSmooth)
            {
                textureMagFilter = Convert.ToInt32(TextureMagFilter.Linear);
                textureMinFilter = Convert.ToInt32(TextureMinFilter.Linear);
            }
            else
            {
                textureMagFilter = Convert.ToInt32(TextureMagFilter.Nearest);
                textureMinFilter = Convert.ToInt32(TextureMinFilter.Nearest);
            }

            BeginUse();
            GL.TexParameter(TextureTarget.TextureRectangle, TextureParameterName.TextureMagFilter, textureMagFilter);
            GL.TexParameter(TextureTarget.TextureRectangle, TextureParameterName.TextureMinFilter, textureMinFilter);
            GL.TexImage2D(TextureTarget.TextureRectangle, 0, internalFormat, width, height, 0, inputFormat, PixelType.UnsignedByte, pixels);
            Width = width;
            Height = height;
            IsSmooth = isSmooth;
            EndUse();
        }

        /// <summary>
        /// Starts using the texture.
        /// </summary>
        public void BeginUse()
        {
            GL.Enable(EnableCap.TextureRectangle);
            GL.BindTexture(TextureTarget.TextureRectangle, Id);
        }

        /// <summary>
        /// Ends using the texture.
        /// </summary>
        public void EndUse()
        {
            GL.Disable(EnableCap.TextureRectangle);
        }
    }
}