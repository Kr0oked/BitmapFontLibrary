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
using BitmapFontLibrary.Loader;
using BitmapFontLibrary.Renderer;

namespace BitmapFontLibrary
{
    /// <summary>
    /// Draws texts with bitmap fonts.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class BitmapFont
    {
        private readonly IFontLoader _fontLoader;
        private readonly IFontRenderer _fontRenderer;

        /// <summary>
        /// Draws texts with bitmap fonts.
        /// </summary>
        /// <param name="fontLoader">Object of a class that implements the IFontLoader interface</param>
        /// <param name="fontRenderer">Object of a class that implements the IFontRenderer interface</param>
        public BitmapFont(IFontLoader fontLoader, IFontRenderer fontRenderer)
        {
            if (fontLoader == null) throw new ArgumentNullException("fontLoader");
            if (fontRenderer == null) throw new ArgumentNullException("fontRenderer");
            _fontLoader = fontLoader;
            _fontRenderer = fontRenderer;
        }

        /// <summary>
        /// Initializes the bitmap font.
        /// </summary>
        /// <param name="path">Path to a Angelcode Bitmap Font file</param>
        public void Initialize(string path)
        {
            _fontRenderer.Initialize(_fontLoader.Load(path));
        }

        /// <summary>
        /// Draws a text.
        /// </summary>
        /// <param name="text">The text to draw</param>
        /// <param name="x">The x-coordinate to start the text</param>
        /// <param name="y">The y-coordinate to start the text</param>
        /// <param name="z">The z-coordinate to start the text</param>
        /// <param name="size">The size of the text</param>
        public void Draw(string text, float x, float y, float z, float size)
        {
            _fontRenderer.Render(text, x, y, z, size);
        }
    }
}