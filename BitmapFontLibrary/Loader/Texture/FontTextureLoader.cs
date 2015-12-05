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
using System.Drawing;
using System.Drawing.Imaging;
using BitmapFontLibrary.Loader.Exception;
using BitmapFontLibrary.Model;
using OpenTK.Graphics.OpenGL;
using GlPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using SystemPixelFormat = System.Drawing.Imaging.PixelFormat;

namespace BitmapFontLibrary.Loader.Texture
{
    /// <summary>
    /// Loader for font textures.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
    public class FontTextureLoader : IFontTextureLoader
    {
        /// <summary>
        /// Loads a font texture.
        /// </summary>
        /// <param name="path">Path to the font texture</param>
        /// <param name="isSmooth">True if smoothing was turned on during creation, otherwise false</param>
        /// <returns>The loaded font texture</returns>
        public IFontTexture Load(string path, bool isSmooth)
        {
            try
            {
                if (path == null) throw new ArgumentNullException("path");
                var bitmap = new Bitmap(path);
                
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                    bitmap.PixelFormat);

                var internalFormat = GetInternalFormat(bitmapData.PixelFormat);
                var inputFormat = GetInputFormat(bitmapData.PixelFormat);

                var texture = new FontTexture();
                texture.Initialize(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, isSmooth, internalFormat, inputFormat);
                
                bitmap.UnlockBits(bitmapData);
                return texture;
            }
            catch (ArgumentException e)
            {
                throw new FontLoaderException("Could not find page file", e);
            }
        }

        /// <summary>
        /// Determines the input pixel format to use.
        /// </summary>
        /// <param name="pixelFormat">Pixel format of the source</param>
        /// <returns>The input pixel format to use</returns>
        private static GlPixelFormat GetInputFormat(SystemPixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case SystemPixelFormat.Format24bppRgb:
                    return GlPixelFormat.Bgr;
                case SystemPixelFormat.Format32bppArgb:
                    return GlPixelFormat.Bgra;
                default:
                    throw new FontLoaderException("Unsupported pixel format: " + pixelFormat);
            }
        }

        /// <summary>
        /// Determines the internal pixel format to use.
        /// </summary>
        /// <param name="pixelFormat">Pixel format of the source</param>
        /// <returns>The internal pixel format to use</returns>
        private static PixelInternalFormat GetInternalFormat(SystemPixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case SystemPixelFormat.Format24bppRgb:
                    return PixelInternalFormat.Rgb;
                case SystemPixelFormat.Format32bppArgb:
                    return PixelInternalFormat.Rgba;
                default:
                    throw new FontLoaderException("Unsupported pixel format: " + pixelFormat);
            }
        }
    }
}