using Gtk;
using System.Threading.Tasks;

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
            var runner = new PictureFrameRunner(win);
            await runner.Run();
        }
    }
}
