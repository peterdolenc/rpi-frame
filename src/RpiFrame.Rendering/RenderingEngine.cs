using System;
using System.Drawing;
using System.Threading.Tasks;
using RpiFrame.Entities;
using RpiFrame.Interfaces;


namespace RpiFrame.Rendering
{
    public class RenderingEngine
    {
        private readonly Settings _settings;
        private readonly IImageWindow _window;
        private int _lastStepRenderTime;

        public RenderingEngine(Settings settings, IImageWindow window)
        {
            this._settings = settings;
            this._window = window;
            this._lastStepRenderTime = 80; // 80ms default
        }

        /// <summary>
        /// Renders frames of media (image, video, widgets)
        /// </summary>
        public async Task Render(MediaFile mediaFile)
        {
            var props = AnalyzeNewImage(mediaFile);
            var end = DateTime.UtcNow.AddSeconds(_settings.Duration);
            double availableMiliseconds;

            Console.WriteLine("---");
            Console.WriteLine($"New render, duration set to {_settings.Duration} seconds");
            Console.WriteLine($"Fitment {props.Fitment.ToString()}");
            Console.WriteLine("---");
            _window.LoadImage(props.Image.ToImageBytes());

            do
            {
                availableMiliseconds = Math.Max(end.Subtract(DateTime.UtcNow).TotalMilliseconds, 0);
                Console.WriteLine($"Before rendering we have {availableMiliseconds} ms available");
                int stepsMade = await Redraw(props, (int)availableMiliseconds);
                Console.WriteLine($"After rendering we have {end.Subtract(DateTime.UtcNow).TotalMilliseconds} ms available");
            } while (!props.ScrollingDone);

        }

        /// <summary>
        /// Start sliding the image in increments so that one slide is achieved per set interval
        /// Make corrections - if rendering itself took too long, then slide the picture further forward
        /// </summary>
        private async Task<int> Redraw(RenderingProperties props, int availableMiliseconds)
        {
            if (props.Fitment == Fitment.Still)
            {
                //_window.LoadImage(props.Image.ToImageBytes());
                await Task.Delay(availableMiliseconds);

                return 0;
            }

            // Scrolling
            else {
                var startTime = DateTime.UtcNow;

                // Step size depends on the speed of the scrolling
                int timeLeftInSteps = (int)Math.Max(1,Math.Ceiling((double)availableMiliseconds / Math.Max(_lastStepRenderTime, 1)));
                int stepsJumpAmount = (int)Math.Ceiling((double)props.StepsLeft / timeLeftInSteps);
                props.CurrentStep += stepsJumpAmount;

                // Crop the image and render it
                int x = 0, y = 0;
                if (props.Fitment == Fitment.HorizontalScroll) {
                    x = stepsJumpAmount;
                } else if (props.Fitment == Fitment.VerticalScroll) {
                    y = stepsJumpAmount;
                }
                //var cropped = props.Image.CropTo(_settings.ScreenWidth, _settings.ScreenHeight, x, y);
                //_window.LoadImage(cropped.ToImageBytes());
                _window.Scroll(x,y);

                double renderTime = DateTime.UtcNow.Subtract(startTime).TotalMilliseconds;
                _lastStepRenderTime = (int)(renderTime / stepsJumpAmount);
                Console.WriteLine($"Step render time set to {_lastStepRenderTime} since {stepsJumpAmount} steps were done in {renderTime} ms.");

                // If last step render time is smaller that time supposedly left for one step then add some delay
                if (timeLeftInSteps > props.StepsLeft && props.CurrentStep > stepsJumpAmount) {
                    int additionalDelay = (int)Math.Floor((availableMiliseconds - ((double)props.StepsLeft * _lastStepRenderTime))/props.StepsLeft);
                    await Task.Delay(Math.Max(additionalDelay, 0));
                    Console.WriteLine($"Additional delay of {Math.Max(additionalDelay, 0)} ms inserted");
                }

                return stepsJumpAmount;
            }
        }

        /// <summary>
        /// Convert the image, fit it and resize it
        /// </summary>
        private RenderingProperties AnalyzeNewImage(MediaFile mediaFile)
        {
            var image = mediaFile.DataBuffer.ToBitmap();
            var props = FitAndResize(image);

            return props;
        }

        /// <summary>
        /// Determine how we are fitting the image - by width or by height (or full image)
        /// Determine how much you have to zoom in in order to keep the black area in the range of the setting
        /// Determine - if we animate by a single pixel - how many steps do we have in animation
        /// </summary>
        private RenderingProperties FitAndResize(Bitmap image)
        {
            var props = new RenderingProperties();

            double imageRatio = (double)image.Width / image.Height;
            double screenRatio = _settings.ScreenRatio;

            int resizeWidth = _settings.ScreenWidth;
            int resizeHeight = _settings.ScreenHeight;

            // Portrait (or just square)/narrow image => Screen is wider than the image
            if (screenRatio > imageRatio)
            {
                // Natural fitment would be to fit by height and have some black bars on the side
                props.Fitment = Fitment.Still;
                resizeWidth = (int)(resizeHeight * imageRatio);

                // If we have black area reducing settings enabled - we must check if any of the cases are met
                if (_settings.AllowedPortraitImageBlackArea.Enabled) {
                    var blackArea = (float)(_settings.ScreenWidth - resizeWidth) / _settings.ScreenWidth;

                    // If minimum black area should be eliminated then we fit this image by width 
                    // and let the height of the image be higher than the screen height
                    bool isBellowMinBlackArea = _settings.AllowedPortraitImageBlackArea.Min > 0 && 
                                                         _settings.AllowedPortraitImageBlackArea.Min > blackArea;
                    // Or if the black area exceeds max amount of black area allowed we also zoom in and let the image 
                    // be scrolled vertically
                    bool isAboveMaxBlackArea = _settings.AllowedPortraitImageBlackArea.Max < 1 && 
                                                        _settings.AllowedPortraitImageBlackArea.Max < blackArea;

                    // In either case we do vertical scrolling
                    if (isBellowMinBlackArea || isAboveMaxBlackArea) {
                        props.Fitment = Fitment.VerticalScroll;

                        // For bellow min fitment image is fitted by width
                        if (isBellowMinBlackArea) {
                            resizeWidth = _settings.ScreenWidth;
                        } 

                        // For above max fitment width is screen width minus the maximum allowed amount of black area
                        else if (isAboveMaxBlackArea) {
                            resizeWidth = (int)(_settings.ScreenWidth * (1.0 - _settings.AllowedPortraitImageBlackArea.Max));
                        }

                        resizeHeight = (int)(resizeWidth / imageRatio);
                        props.MaxSteps = resizeHeight - _settings.ScreenHeight;
                    }
                }
            }

            // Panoramic, ultra wide image => Screen is narrower than the image
            else {
                // Natural fitment would be to fit by width and have some black bars on the top-bottom
                props.Fitment = Fitment.Still;
                resizeHeight = (int)(resizeWidth / imageRatio);

                // If we have black area reducing settings enabled - we must check if any of the cases are met
                if (_settings.AllowedWideImageBlackArea.Enabled)
                {
                    var blackArea = (float)(_settings.ScreenHeight - resizeHeight) / _settings.ScreenHeight;

                    // If minimum black area should be eliminated then we fit this image by height 
                    // and let the width of the image be wider than the screen width
                    bool isBellowMinBlackArea = _settings.AllowedWideImageBlackArea.Min > 0 &&
                                                         _settings.AllowedWideImageBlackArea.Min > blackArea;
                    // Or if the black area exceeds max amount of black area allowed we also zoom in and let the image 
                    // be scrolled horizontally
                    bool isAboveMaxBlackArea = _settings.AllowedWideImageBlackArea.Max < 1 &&
                                                        _settings.AllowedWideImageBlackArea.Max < blackArea;

                    // In either case we do horizontal scrolling
                    if (isBellowMinBlackArea || isAboveMaxBlackArea)
                    {
                        props.Fitment = Fitment.HorizontalScroll;

                        // For bellow min fitment image is fitted by width
                        if (isBellowMinBlackArea)
                        {
                            resizeHeight = _settings.ScreenHeight;
                        }

                        // For above max fitment width is screen width minus the maximum allowed amount of black area
                        else if (isAboveMaxBlackArea)
                        {
                            resizeHeight = (int)(_settings.ScreenHeight * (1.0 - _settings.AllowedWideImageBlackArea.Max));
                        }

                        resizeWidth = (int)(resizeHeight * imageRatio);
                        props.MaxSteps = resizeWidth - _settings.ScreenWidth;
                    }
                }
            }

            props.Image = image.ResizeTo(resizeWidth, resizeHeight);;

            return props;
        }
    }
}
