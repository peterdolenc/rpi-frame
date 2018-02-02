using System;
using System.Linq;
using Gtk;

namespace RpiFrame
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();

            var mds = new MediaDiscoveryService();
            var images = mds.Discover();

            win.LoadImage(images.First());


            Application.Run();
        }
    }
}
