using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.IO;
using System.Reflection;
using BitmapFontLibrary;
using BitmapFontLibrary.Model;

namespace BitmapFontLibraryExample
{
    /// <summary>
    /// Example Application of the BitmapFontLibrary
    /// </summary>
    internal class Example
    {
        private readonly GameWindow _game;
        private readonly IBitmapFont _bitmapFont;
        private readonly ITextConfiguration _headlineConfiguration;
        private readonly ITextConfiguration _contentConfiguration;

        /// <summary>
        /// Example Application of the BitmapFontLibrary
        /// </summary>
        private Example()
        {
            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyDirectory == null) throw new FileNotFoundException("Assembly");

            _game = new GameWindow(512, 512);

            _bitmapFont = new BitmapFontStandAlone();
            _bitmapFont.Initialize(Path.Combine(assemblyDirectory, @"Data\exampleFont.xml"));

            _headlineConfiguration = new TextConfiguration
            {
                SizeInPixels = 22,
                MaximalWidth = 256.0f,
                Alignment = TextAlignment.Centered
            };
            _contentConfiguration = new TextConfiguration
            {
                SizeInPixels = 16,
                MaximalWidth = 256.0f,
                Alignment = TextAlignment.Centered
            };

            _game.Load += (sender, e) =>
            {
                _game.VSync = VSyncMode.On;
            };

            _game.Resize += (sender, e) =>
            {
                GL.Viewport(0, 0, _game.Width, _game.Height);
            };

            _game.UpdateFrame += (sender, e) => 
            { 
                if (_game.Keyboard[Key.Escape])
                {
                    _game.Exit();
                }
            };

            _game.RenderFrame += (sender, e) =>
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(0.0f, 0.0f, 0.5f, 0.0f);

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0.0, 256.0, 0.0, 256.0, -1.0, 1.0);

                _bitmapFont.Draw("Bitmap Font Example Application", 
                    0.0f, 256.0f, 0.0f, _headlineConfiguration);
                _bitmapFont.Draw("A C# library for rendering Bitmap Fonts\r\nin OpenGL Applications", 
                    0.0f, 200.0f, 0.0f, _contentConfiguration);

                _game.SwapBuffers();
            };
        }

        /// <summary>
        /// Starts the Example Application
        /// </summary>
        private void Run()
        {
            _game.Run(60.0);
        }

        /// <summary>
        /// Entry point of the BitmapFontLibraryExample Application
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var example = new Example();
            example.Run();
        }
    }
}
