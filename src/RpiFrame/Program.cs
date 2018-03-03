using System;
using Gtk;
using System.Threading.Tasks;
using System.Linq;

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
            var engine = new RenderingEngine(new Settings(), win);
            var mediaCollection = mds.Discover();
            var mediaSequencer = new MediaSequencer();

            while (true) {
                foreach (var mediaFile in mediaSequencer.GetNextSequence(mediaCollection.ToList()))
                {
                    engine.Render(mediaFile);
                    Console.WriteLine(mediaFile.Path);
                    await Task.Delay(2000);
                }
            }

        }
    }
}
