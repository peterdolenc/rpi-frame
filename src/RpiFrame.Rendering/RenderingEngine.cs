using System.Drawing;
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
            var image = mediaFile.DataBuffer.ToBitmap();
            Bitmap resized = Resize(image);

            _window.LoadImage(resized.ToImageBytes());
        }

        private Bitmap Resize(Bitmap image)
        {
            double imageRatio = (double)image.Width / image.Height;
            double screenRatio = (double)_settings.ScreenWidth / _settings.ScreenHeight;
            int resizeWidth = _settings.ScreenWidth;
            int resizeHeight = _settings.ScreenHeight;

            if (screenRatio > imageRatio) {
                // screen is wider - fit by height
                resizeWidth = (int)(resizeHeight * imageRatio);
            } else {
                // screen is higher - fit by width
                resizeHeight = (int)(resizeWidth / imageRatio);
            }

            return image.ResizeTo(resizeWidth, resizeHeight);
        }
    }
}
