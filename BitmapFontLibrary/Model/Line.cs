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
    /// A line of text.
    /// </summary>
    public class Line : ILine
    {
        /// <summary>
        /// The text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// X position of the text.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y position of the text.
        /// </summary>
        public float Y { get; set; }

        public Line()
        {
            Text = "";
            X = 0.0f;
            Y = 0.0f;
        }

        /// <summary>
        /// Checks if another line is equal.
        /// </summary>
        /// <param name="other">The other line</param>
        /// <returns>true if the lines are equal, otherwise false</returns>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        protected bool Equals(Line other)
        {
            return string.Equals(Text, other.Text) && X.Equals(other.X) && Y.Equals(other.Y);
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
            return Equals((Line) obj);
        }

        /// <summary>
        /// Returns the hashcode of the object.
        /// </summary>
        /// <returns>The hashcode of the object</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                return hashCode;
            }
        }
    }
}