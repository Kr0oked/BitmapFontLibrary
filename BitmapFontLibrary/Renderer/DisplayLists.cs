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

namespace BitmapFontLibrary.Renderer
{
    /// <summary>
    /// Display lists to store OpenGL commands.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DisplayLists : IDisplayLists, IDisposable
    {
        private int _size;
        private int _displayLists;

        /// <summary>
        /// Initializes new display lists.
        /// </summary>
        /// <param name="size">Size of the display lists</param>
        public void Initialize(int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException("size");
            DeleteDisplayLists();
            _size = size;
            _displayLists = GL.GenLists(size);
        }

        /// <summary>
        /// Begin a new display list.
        /// </summary>
        /// <param name="index">The index of the display list</param>
        public void BeginList(int index)
        {
            if (!IsInitialized()) throw new FieldAccessException("DisplayLists are not initialized");
            if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException("index");
            GL.NewList(_displayLists + index, ListMode.Compile);
        }

        /// <summary>
        /// Ends a new display list.
        /// </summary>
        public void EndList()
        {
            if (!IsInitialized()) throw new FieldAccessException("DisplayLists are not initialized");
            GL.EndList();
        }

        /// <summary>
        /// Calls a display list.
        /// </summary>
        /// <param name="index">The index of the list</param>
        public void CallList(int index)
        {
            if (!IsInitialized()) throw new FieldAccessException("DisplayLists are not initialized");
            if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException("index");
            GL.CallList(_displayLists + index);
        }

        /// <summary>
        /// Returns if the display lists are initialized.
        /// </summary>
        /// <returns>true if the display lists are initialized, otherwise false</returns>
        public bool IsInitialized()
        {
            return _size > 0 && _displayLists > 0;
        }

        /// <summary>
        /// Disposes all used resources.
        /// </summary>
        public void Dispose()
        {
            DeleteDisplayLists();
        }

        /// <summary>
        /// Deletes the display lists.
        /// </summary>
        private void DeleteDisplayLists()
        {
            if (_displayLists != 0)
            {
                GL.DeleteLists(_displayLists, _size);
            }
        }
    }
}