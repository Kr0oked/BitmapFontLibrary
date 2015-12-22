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
using System.Collections.Generic;
using BitmapFontLibrary.Helper;
using BitmapFontLibrary.Model;

namespace BitmapFontLibrary.Renderer
{
    /// <summary>
    /// Calculator of lines.
    /// </summary>
    public class LineCalculator : ILineCalculator
    {
        private readonly ICharAdapter _charAdapter;
        private ITextConfiguration _textConfiguration;
        private IFont _font;
        private List<ILine> _lines;
        private Line _currentLine;
        private float _lineLength;
        private float _textHeight;
        private float _lineNumber;
        private float _scalingFactor;
        private int _lineHeightConsumption;

        /// <summary>
        /// Calculator of lines.
        /// </summary>
        /// <param name="charAdapter">Object of a class that implements the ICharAdapter interface</param>
        public LineCalculator(ICharAdapter charAdapter)
        {
            if (charAdapter == null) throw new ArgumentNullException("charAdapter");
            _charAdapter = charAdapter;
        }

        /// <summary>
        /// Calculates lines from a text.
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="textConfiguration">The configuration of the text</param>
        /// <param name="font">The font of the text</param>
        /// <returns></returns>
        public List<ILine> CalculateLines(string text, ITextConfiguration textConfiguration, IFont font)
        {
            _textConfiguration = textConfiguration;
            _font = font;
            _lines = new List<ILine>();
            _currentLine = new Line();
            _lineLength = 0.0f;
            _textHeight = 0.0f;
            _lineNumber = 0;
            _scalingFactor = _textConfiguration.SizeInPixels*1.0f/_font.Size;
            _lineHeightConsumption = _font.LineHeight + _font.BaseHeight;
            
            ProcessCharacters(text);

            return _lines;
        }

        /// <summary>
        /// Loops over all characters in the text and builds lines with them.
        /// </summary>
        /// <param name="text">The text</param>
        private void ProcessCharacters(string text)
        {
            var previousCharId = -1;

            foreach (var textCharacter in text)
            {
                var charId = _charAdapter.CharToIntCharValue(textCharacter);

                if (previousCharId == 13 && charId == 10)
                {
                    if (IsTextTooHigh()) break;
                    StartNewLine();
                }
                else
                {
                    var character = _font.GetCharacterById(charId);
                    if (character != null)
                    {
                        var kerningAmount = _font.GetKerningAmount(previousCharId, charId);
                        var characterWidthConsumption = kerningAmount + character.XOffset + character.XAdvance;

                        if (IsLineTooLong(characterWidthConsumption))
                        {
                            if (IsTextTooHigh()) break;
                            StartNewLine();
                            AddCharacter(textCharacter, characterWidthConsumption);
                        }
                        else
                        {
                            AddCharacter(textCharacter, characterWidthConsumption);
                        }
                    }
                }

                previousCharId = charId;
            }

            StartNewLine();
        }

        /// <summary>
        /// Calculates the x position of a Line.
        /// </summary>
        /// <returns>The calculated x position</returns>
        private float CalculateX()
        {
            float indentation;
            switch (_textConfiguration.Alignment)
            {
                case TextAlignment.LeftAligned:
                    indentation = 0.0f;
                    break;
                case TextAlignment.Centered:
                    indentation = (_textConfiguration.MaximalWidth - _lineLength) / 2;
                    break;
                case TextAlignment.RightAligned:
                    indentation = _textConfiguration.MaximalWidth - _lineLength;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return indentation;
        }

        /// <summary>
        /// Calculates the y position of a Line.
        /// </summary>
        /// <returns>The calculated y position</returns>
        private float CalculateY()
        {
            return (_font.LineHeight + _font.BaseHeight)*_lineNumber*_scalingFactor;
        }

        /// <summary>
        /// Measures if the text gets too high with the next line.
        /// </summary>
        /// <returns>True if the text gets too high otherwise false</returns>
        private bool IsTextTooHigh()
        {
            return !(_textHeight + _lineHeightConsumption*_scalingFactor < _textConfiguration.MaximalHeight);
        }

        /// <summary>
        /// Measures if the current line gets too long with the next character.
        /// </summary>
        /// <param name="characterWidthConsumption">The width which gets consumed from the character</param>
        /// <returns>True if the line gets too long otherwise false</returns>
        private bool IsLineTooLong(int characterWidthConsumption)
        {
            return !(_lineLength + characterWidthConsumption*_scalingFactor < _textConfiguration.MaximalWidth);
        }

        /// <summary>
        /// Determines the current line and starts a new one.
        /// </summary>
        private void StartNewLine()
        {
            _textHeight += _lineHeightConsumption * _scalingFactor;
            _currentLine.X = CalculateX();
            _currentLine.Y = CalculateY();
            _lines.Add(_currentLine);
            _currentLine = new Line();
            _lineLength = 0.0f;
            _lineNumber++;
        }

        /// <summary>
        /// Adds a character to the current line.
        /// </summary>
        /// <param name="character">The character to add</param>
        /// <param name="characterWidthConsumption">The width which gets consumed from the character</param>
        private void AddCharacter(char character, int characterWidthConsumption)
        {
            _currentLine.Text += character;
            _lineLength += characterWidthConsumption * _scalingFactor;
        }
    }
}