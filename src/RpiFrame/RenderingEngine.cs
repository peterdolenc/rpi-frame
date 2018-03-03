using System;
using RpiFrame.Entities;

namespace RpiFrame
{
    public class RenderingEngine
    {
        private readonly Settings _settings;
        private readonly MainWindow _window;

        public RenderingEngine(Settings settings, MainWindow window)
        {
            this._settings = settings;
            this._window = window;
        }


        public void Render(MediaFile mediaFile) {
            _window.LoadImage(mediaFile.Pixbuf);
        }


    }
}
