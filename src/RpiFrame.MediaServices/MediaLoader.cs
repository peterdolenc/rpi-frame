using RpiFrame.Entities;

namespace RpiFrame.MediaServices
{
    public static class MediaLoader
    {
        public static MediaFile LoadImage(MediaFileHeader metadata) {
            var buffer = System.IO.File.ReadAllBytes(metadata.Path);

            var mediaFile = new MediaFile()
            {
                Metadata = metadata,
                DataBuffer = buffer
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
