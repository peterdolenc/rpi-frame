using System;
using Gtk;
using System.Threading.Tasks;
using System.Linq;
using RpiFrame.Entities;

namespace RpiFrame
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();

            Task.Run(() => RunPictureFrame(win));
                
            Application.Run();
        }

        public static async Task RunPictureFrame(MainWindow win) {
            var mds = new MediaDiscoveryService();
            var engine = new RenderingEngine(new RpiFrame.Entities.Settings(), win);
            var mediaCollection = mds.Discover();
            var mediaSequencer = new MediaSequencer();

            mediaSequencer.PrepareNextSequence(mediaCollection);

            while (true) {
                var sequence = await mediaSequencer.GetNextSequence();
                mediaSequencer.PrepareNextSequence(mediaCollection);
                foreach (var mediaFile in sequence)
                {
                    engine.Render(mediaFile);
                    Console.WriteLine(mediaFile.Metadata.Path);
                    await Task.Delay(2000);
                }
            }

        }
    }
}
