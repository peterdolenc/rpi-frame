﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RpiFrame
{
    public class MediaDiscoveryService
    {
        public MediaDiscoveryService()
        {

           
        }


        public IEnumerable<string> Discover()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var samplesDir = Path.Combine(currentDir, "../../../samples");

            Console.WriteLine(samplesDir);

            var files = Directory.EnumerateFiles(samplesDir).Where(f => f.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase));

            foreach (var file in files) {
                Console.WriteLine(file);
            }

            return files;
        }
    }
}