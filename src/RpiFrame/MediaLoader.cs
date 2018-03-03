
namespace RpiFrame
{
    public static class MediaLoader
    {

        public static void LoadImageToPixbuf(MediaFile mediaFile) {
            var buffer = System.IO.File.ReadAllBytes(mediaFile.Path);
            var pixbuf = new Gdk.Pixbuf(buffer);

            mediaFile.Pixbuf = pixbuf;
        }

        public static MediaFile LoadImageMetadata(string path)
        {
            var buffer = System.IO.File.ReadAllBytes(path);
            var pixbuf = new Gdk.Pixbuf(buffer);

            var mediaFile = new MediaFile()
            {
                Pixbuf = pixbuf,
                Path = path
            };

            return mediaFile;
        }


    }
}
