using System;
using Gtk;
using RpiFrame.Interfaces;

public partial class MainWindow : Gtk.Window, IImageWindow
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        KeyPressEvent += (o, args) => {
            if (args.Event.Key == Gdk.Key.Escape || args.Event.Key == Gdk.Key.KP_Space) {
                Application.Quit();
            }
        };
        this.Fullscreen();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    public void LoadImage(byte[] buffer) {
        var pixbuf = new Gdk.Pixbuf(buffer);
        MainImage.Pixbuf = pixbuf;
    }

}
