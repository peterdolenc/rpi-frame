using RpiFrame.Entities;

namespace RpiFrame.MediaServices
{
    public class SettingsService
    {
        public SettingsService()
        {
        }

        public Settings CurrentSettings { get; } = new Settings();

  
        public void SetScreenDimensions(int defaultWidth, int defaultHeight)
        {
            CurrentSettings.ScreenWidth = defaultWidth;
            CurrentSettings.ScreenHeight = defaultHeight;
        }
    }
}
