using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RpiFrame.Entities;

namespace RpiFrame
{
    public class MediaDiscoveryService
    {
        public MediaDiscoveryService()
        {
        }

        public IEnumerable<MediaFileHeader> Discover()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var samplesDir = Path.Combine(currentDir, "../../../samples");

            Console.WriteLine(samplesDir);

            var files = Directory.EnumerateFiles(samplesDir).Where(f => f.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase));

            foreach (var file in files) {
                Console.WriteLine(file);
            }

            return files.Select(MediaLoader.LoadImageMetadata);
        }
    }
}
