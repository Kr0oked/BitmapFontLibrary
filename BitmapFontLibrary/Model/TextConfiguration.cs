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

namespace BitmapFontLibrary.Model
{
    /// <summary>
    /// Configuration of a text.
    /// </summary>
    public class TextConfiguration : ITextConfiguration
    {
        private const float EmsToPointsScaleFactor = 12.0f;
        private const float EmsToPixelsScaleFactor = 16.0f;
        private const float EmsToPercentScaleFactor = 100.0f;
        private float _sizeInEms;
        private uint _sizeInPoints;
        private uint _sizeInPixels;
        private uint _sizeInPercent;

        /// <summary>
        /// Configuration of a text.
        /// </summary>
        public TextConfiguration()
        {
            SizeInEms = 1.0f;
        }

        /// <summary>
        /// The size of the font in em.
        /// </summary>
        public float SizeInEms
        {
            get { return _sizeInEms; }
            set
            {
                _sizeInEms = value;
                _sizeInPoints = SizeInEmsToSizeInPoints(_sizeInEms);
                _sizeInPixels = SizeInEmsToSizeInPixels(_sizeInEms);
                _sizeInPercent = SizeInEmsToSizeInPercent(_sizeInEms);
            }
        }

        /// <summary>
        /// The size of the font in pt.
        /// </summary>
        public uint SizeInPoints
        {
            get { return _sizeInPoints; }
            set 
            {
                _sizeInPoints = value;
                _sizeInEms = SizeInPointsToSizeInEms(_sizeInPoints);
                _sizeInPixels = SizeInEmsToSizeInPixels(_sizeInEms);
                _sizeInPercent = SizeInEmsToSizeInPercent(_sizeInEms);
            }
        }

        /// <summary>
        /// The size of the font in px.
        /// </summary>
        public uint SizeInPixels
        {
            get { return _sizeInPixels; }
            set
            {
                _sizeInPixels = value;
                _sizeInEms = SizeInPixelsToSizeInEms(_sizeInPixels);
                _sizeInPoints = SizeInEmsToSizeInPoints(_sizeInEms);
                _sizeInPercent = SizeInEmsToSizeInPercent(_sizeInEms);
            }
        }

        /// <summary>
        /// The size of the font in %.
        /// </summary>
        public uint SizeInPercent
        {
            get { return _sizeInPercent; }
            set
            {
                _sizeInPercent = value;
                _sizeInEms = SizeInPercentToSizeInEms(_sizeInPercent);
                _sizeInPoints = SizeInEmsToSizeInPoints(_sizeInEms);
                _sizeInPixels = SizeInEmsToSizeInPixels(_sizeInEms);
            }
        }

        /// <summary>
        /// Converts a size in points to a size in ems.
        /// </summary>
        /// <param name="sizeInPoints">Size in points</param>
        /// <returns>Size in ems</returns>
        private static float SizeInPointsToSizeInEms(uint sizeInPoints)
        {
            return sizeInPoints/EmsToPointsScaleFactor;
        }

        /// <summary>
        /// Converts a size in pixels to a size in ems.
        /// </summary>
        /// <param name="sizeInPixels">Size in pixels</param>
        /// <returns>Size in ems</returns>
        private static float SizeInPixelsToSizeInEms(uint sizeInPixels)
        {
            return sizeInPixels/EmsToPixelsScaleFactor;
        }

        /// <summary>
        /// Converts a size in percents to a size in ems.
        /// </summary>
        /// <param name="sizeInPercent">Size in percents</param>
        /// <returns>Size in ems</returns>
        private static float SizeInPercentToSizeInEms(uint sizeInPercent)
        {
            return sizeInPercent/EmsToPercentScaleFactor;
        }

        /// <summary>
        /// Converts a size in ems to a size in points.
        /// </summary>
        /// <param name="sizeInEms">Size in ems</param>
        /// <returns>Size in points</returns>
        private static uint SizeInEmsToSizeInPoints(float sizeInEms)
        {
            return Convert.ToUInt32(sizeInEms * EmsToPointsScaleFactor);
        }

        /// <summary>
        /// Converts a size in ems to a size in pixels.
        /// </summary>
        /// <param name="sizeInEms">Size in ems</param>
        /// <returns>Size in pixels</returns>
        private static uint SizeInEmsToSizeInPixels(float sizeInEms)
        {
            return Convert.ToUInt32(sizeInEms*EmsToPixelsScaleFactor);
        }

        /// <summary>
        /// Converts a size in ems to a size in percents.
        /// </summary>
        /// <param name="sizeInEms">Size in ems</param>
        /// <returns>Size in percents</returns>
        private static uint SizeInEmsToSizeInPercent(float sizeInEms)
        {
            return Convert.ToUInt32(sizeInEms*EmsToPercentScaleFactor);
        }
    }
}