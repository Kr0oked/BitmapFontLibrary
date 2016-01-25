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

using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Loader;
using BitmapFontLibrary.Loader.Parser;
using BitmapFontLibrary.Loader.Parser.Binary;
using BitmapFontLibrary.Loader.Parser.Text;
using BitmapFontLibrary.Loader.Parser.Xml;
using BitmapFontLibrary.Loader.Texture;
using BitmapFontLibrary.Model;
using BitmapFontLibrary.Renderer;
using Ninject.Modules;

namespace BitmapFontLibrary
{
    /// <summary>
    /// Ninject module of the BitmapFontLibrary.
    /// </summary>
    public class BitmapFontModule : NinjectModule
    {
        /// <summary>
        /// Loads the bindings.
        /// </summary>
        public override void Load()
        {
            // BitmapFont Library
            Bind<BitmapFont>().ToSelf();
            Bind<IBitmapFont>().To<BitmapFont>();

            // Helper
            Bind<ICharAdapter>().To<CharAdapter>().InThreadScope();
            Bind<IIntAdapter>().To<IntAdapter>().InThreadScope();
            Bind<IStringAdapter>().To<StringAdapter>().InThreadScope();

            // Loader
            Bind<IFontLoader>().To<FontLoader>().InThreadScope();
            Bind<IFontFileParser>().To<BinaryFontFileParser>().InThreadScope().Named("Binary");
            Bind<IFontFileParser>().To<TextFontFileParser>().InThreadScope().Named("Text");
            Bind<ITextReader>().To<TextReader>().InThreadScope();
            Bind<IFontFileParser>().To<XmlFontFileParser>().InThreadScope().Named("Xml");
            Bind<IXmlSchemaReader>().To<XmlSchemaReader>().InThreadScope();
            Bind<IXmlSettingsBuilder>().To<XmlSettingsBuilder>().InThreadScope();
            Bind<IFontTextureLoader>().To<FontTextureLoader>().InThreadScope();

            // Model
            Bind<ICharacter>().To<Character>();
            Bind<IFont>().To<Font>();
            Bind<IFontTexture>().To<FontTexture>();
            Bind<ITextConfiguration>().To<TextConfiguration>();

            // Renderer
            Bind<ICharacterSprites>().To<CharacterSprites>();
            Bind<IDisplayLists>().To<DisplayLists>();
            Bind<IFontAlign>().To<FontAlign>();
            Bind<IFontRenderer>().To<FontRenderer>();
            Bind<ILineCalculator>().To<LineCalculator>();
        }
    }
}