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
using BitmapFontLibrary.Model;
using OpenTK.Graphics.OpenGL;

namespace BitmapFontLibrary.Renderer
{
    /// <summary>
    /// Sprites for drawing characters.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class CharacterSprites : ICharacterSprites
    {
        private readonly IDisplayLists _displayLists;

        /// <summary>
        /// Sprites for drawing characters.
        /// </summary>
        /// <param name="displayLists">Object of a class that implements the IDisplayLists interface</param>
        public CharacterSprites(IDisplayLists displayLists)
        {
            if (displayLists == null) throw new ArgumentNullException("displayLists");
            _displayLists = displayLists;
        }

        /// <summary>
        /// Initializes new sprites.
        /// </summary>
        /// <param name="size">Size of the sprites</param>
        public void Initialize(int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException("size");
            _displayLists.Initialize(size);
        }

        /// <summary>
        /// Adds a new character to the sprites.
        /// </summary>
        /// <param name="index">The index of the character</param>
        /// <param name="character">The character to add</param>
        public void AddCharacterSprite(int index, ICharacter character)
        {
            if (character == null) throw new ArgumentNullException("character");
            _displayLists.BeginList(index);

            GL.Translate(character.XOffset, 0.0f, 0.0f);

            GL.PushMatrix();

            GL.Translate(0.0f, -1 * character.YOffset, 0.0f);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(character.X, character.Y + character.Height);
            GL.Vertex2(0, -1 * character.Height);

            GL.TexCoord2(character.X, character.Y);
            GL.Vertex2(0, 0);

            GL.TexCoord2(character.X + character.Width, character.Y);
            GL.Vertex2(character.Width, 0);

            GL.TexCoord2(character.X + character.Width, character.Y + character.Height);
            GL.Vertex2(character.Width, -1 * character.Height);

            GL.End();

            GL.PopMatrix();

            GL.Translate(character.XAdvance, 0.0f, 0.0f);

            _displayLists.EndList();
        }

        /// <summary>
        /// Renders a character sprite.
        /// </summary>
        /// <param name="index">The index of the character to render its sprite</param>
        public void RenderCharacterSprite(int index)
        {
            _displayLists.CallList(index);
        }
    }
}