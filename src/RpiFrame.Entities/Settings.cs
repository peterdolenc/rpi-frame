
namespace RpiFrame.Entities
{
    public class Settings
    {
        /// <summary>
        /// Duration of single image on the screen
        /// </summary>
        public int Duration { get; set; } = 3;

        public float AllowedEmptyScreenRatio { get; set; } = 0.5f;

        public int ScreenWidth { get; set; } = 1280;

        public int ScreenHeight { get; set; } = 720;
    }
}
