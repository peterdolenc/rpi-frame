using System;
using Gtk;
using RpiFrame.Interfaces;

public partial class MainWindow : Gtk.Window, IImageWindow
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
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
