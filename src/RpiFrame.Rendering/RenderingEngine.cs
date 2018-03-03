using RpiFrame.Entities;
using RpiFrame.Interfaces;


namespace RpiFrame.Rendering
{
    public class RenderingEngine
    {
        private readonly Settings _settings;
        private readonly IImageWindow _window;

        public RenderingEngine(Settings settings, IImageWindow window)
        {
            this._settings = settings;
            this._window = window;
        }

        public void Render(MediaFile mediaFile)
        {
            _window.LoadImage(mediaFile.DataBuffer);
        }



    }
}
