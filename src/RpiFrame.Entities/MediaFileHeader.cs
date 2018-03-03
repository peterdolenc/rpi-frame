using System;

namespace RpiFrame.Entities
{
    public class MediaFileHeader
    {
        public string Path { get; set; }

        public DateTime DateTaken { get; set; }

        public bool IsVideo { get; set; }
    }
}
