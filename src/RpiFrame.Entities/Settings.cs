
namespace RpiFrame.Entities
{
    public class Settings
    {
        /// <summary>
        /// Duration of single image on the screen in seconds
        /// </summary>
        public int Duration { get; set; } = 10;

        /// <summary>
        /// This setting is basically inverted setting how much to zoom-in wide images
        /// 
        /// If image is wider than the screen, then black bars on top and bottom will appear
        /// Min controls the scenario when image is just slightly wider than the screen and by setting allowed amount to non-zero you can
        /// avoid small black bars in these almost-fit images
        /// Min 0.1 will zoom the image in slightly when image is only 10% wider than the screen
        /// Max controls the images that are way wider than the screen and determines how much these will be zoomed out (black bars) in 
        ///  desire to fit them on one screen.
        /// Max 0.6 will allow only 40% of the screen to be taken by the image (wery thin/wide image)
        /// </summary>
        public RangeSetting AllowedWideImageBlackArea = new RangeSetting(0.05, 0.1);

        /// <summary>
        /// This setting is basically inverted setting for how much to zoom-in portrait images
        /// 
        /// If image is narrower than the screen, then black bars on left and right will appear
        /// Min controls the scenario when image is just slightly higher than the screen and by setting allowed amount to non-zero you can
        /// avoid small black bars in these almost-fit images
        /// Min 0.1 will zoom the image in slightly when image is only 10% higher than the screen
        /// Max controls the images that are way taller than the screen and determines how much these will be zoomed out (black bars) in 
        ///  desire to fit them on one screen.
        /// Max 0.6 will allow only 40% of the screen to be taken by the image (normal portrait image on 16:9 screen)
        /// </summary>
        public RangeSetting AllowedPortraitImageBlackArea = new RangeSetting(0.1, 0.1);









        #region set automatically

        public int ScreenWidth { get; set; } = 1280;

        public int ScreenHeight { get; set; } = 720;

        public double ScreenRatio => (double)ScreenWidth / ScreenHeight;

        #endregion
    }
}
