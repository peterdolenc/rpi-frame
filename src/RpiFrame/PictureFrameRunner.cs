using System;
using System.Threading.Tasks;
using RpiFrame.MediaServices;
using RpiFrame.Rendering;

namespace RpiFrame
{
    public class PictureFrameRunner
    {
        private readonly MainWindow _window;

        public PictureFrameRunner(MainWindow win)
        {
            _window = win;
        }

        public async Task Run() {

            var mds = new MediaDiscoveryService();
            var mediaCollection = mds.Discover();
            var mediaSequencer = new MediaSequencer();
            var settingsService = new SettingsService();
            settingsService.SetScreenDimensions(_window.DefaultWidth, _window.DefaultHeight);
            var settings = settingsService.CurrentSettings;
            var engine = new RenderingEngine(settings, _window);


            mediaSequencer.PrepareNextSequence(mediaCollection);

            while (true)
            {
                var sequence = await mediaSequencer.GetNextSequence();
                mediaSequencer.PrepareNextSequence(mediaCollection);
                foreach (var mediaFile in sequence)
                {
                    engine.Render(mediaFile);
                    Console.WriteLine(mediaFile.Metadata.Path);
                    await Task.Delay(settings.Duration * 1000);
                }
            }
        }


    }
}
