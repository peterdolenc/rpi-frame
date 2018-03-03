using System;
namespace RpiFrame
{
    public class MediaFile
    {
        public string Path { get; set; }

        public DateTime DateTaken { get; set; }

        public Gdk.Pixbuf Pixbuf { get; set; }

        public bool IsVideo { get; set; }
    }
}
