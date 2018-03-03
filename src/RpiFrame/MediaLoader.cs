using RpiFrame.Entities;

namespace RpiFrame
{
    public static class MediaLoader
    {
        public static MediaFile LoadImage(MediaFileHeader metadata) {
            var buffer = System.IO.File.ReadAllBytes(metadata.Path);
            var pixbuf = new Gdk.Pixbuf(buffer);

            var mediaFile = new MediaFile()
            {
                Metadata = metadata,
                Pixbuf = pixbuf
            };

            return mediaFile;
        }

        public static MediaFileHeader LoadImageMetadata(string path)
        {
            var metadata = new MediaFileHeader()
            {
                Path = path
            };

            return metadata;
        }


    }
}
