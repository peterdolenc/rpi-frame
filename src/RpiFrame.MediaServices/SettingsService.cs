using RpiFrame.Entities;

namespace RpiFrame.MediaServices
{
    public class SettingsService
    {
        public SettingsService()
        {
        }

        public Settings GetCurrentSettings() {
            return new Settings();
        }
    }
}
