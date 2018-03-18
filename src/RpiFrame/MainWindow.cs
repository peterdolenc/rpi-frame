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

        global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.AlignmentBin[this.MainImage]));
        w1.X = Math.Max(MainImage.Screen.Width - pixbuf.Width, 0)/2;
        w1.Y = Math.Max(MainImage.Screen.Height - pixbuf.Height, 0) / 2;

        MainImage.Pixbuf = pixbuf;
        MainImage.WidthRequest = Math.Min(MainImage.Screen.Width, pixbuf.Width);
        MainImage.HeightRequest = Math.Min(MainImage.Screen.Height, pixbuf.Height);
    }

    public void Scroll(int horizontalAdvancement, int verticalAdvancement) {
        global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.AlignmentBin[this.MainImage]));
        w1.X -= horizontalAdvancement;
        w1.Y -= verticalAdvancement;

        MainImage.WidthRequest += horizontalAdvancement;
        MainImage.HeightRequest += verticalAdvancement;
        MainImage.QueueResizeNoRedraw();
        MainImage.QueueDraw();
        AlignmentBin.QueueDraw();
    }

}
