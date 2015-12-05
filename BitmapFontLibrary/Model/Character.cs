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
    /// Informations about a character.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Character
    {
        /// <summary>
        /// The left position of the character in the texture.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The top position of the character in the texture.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The width of the character image in the texture.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the character image in the texture.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// How much the current position should be offset when copying the image from the texture to the screen.
        /// </summary>
        public int XOffset { get; set; }

        /// <summary>
        /// How much the current position should be offset when copying the image from the texture to the screen.
        /// </summary>
        public int YOffset { get; set; }

        /// <summary>
        /// How much the current position should be advanced after drawing the character.
        /// </summary>
        public int XAdvance { get; set; }

        /// <summary>
        /// The texture page where the character image is found.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The texture channel where the character image is found.
        /// </summary>
        public Channel Channel { get; set; }

        /// <summary>
        /// Informations about a character.
        /// </summary>
        public Character()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            XOffset = 0;
            YOffset = 0;
            XAdvance = 0;
            Page = 0;
            Channel = Channel.All;
        }

        /// <summary>
        /// Checks if another character is equal.
        /// </summary>
        /// <param name="other">The other character</param>
        /// <returns>true if the characters are equal, otherwise false</returns>
        protected bool Equals(Character other)
        {
            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height && XOffset == other.XOffset
                && YOffset == other.YOffset && XAdvance == other.XAdvance && Page == other.Page && Channel == other.Channel;
        }

        /// <summary>
        /// Checks if another object is equal.
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>true if the objects are equal, otherwise false</returns>
        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Character) obj);
        }

        /// <summary>
        /// Returns the hashcode of the object.
        /// </summary>
        /// <returns>The hashcode of the object</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode*397) ^ Y;
                hashCode = (hashCode*397) ^ Width;
                hashCode = (hashCode*397) ^ Height;
                hashCode = (hashCode*397) ^ XOffset;
                hashCode = (hashCode*397) ^ YOffset;
                hashCode = (hashCode*397) ^ XAdvance;
                hashCode = (hashCode*397) ^ Page;
                hashCode = (hashCode*397) ^ (int) Channel;
                return hashCode;
            }
        }
    }
}