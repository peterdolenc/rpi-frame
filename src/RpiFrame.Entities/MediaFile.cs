using System;
namespace RpiFrame.Entities
{
    public class MediaFile
    {
        public MediaFileHeader Metadata { get; set; }

        public byte[] DataBuffer { get; set; }
    }
}
