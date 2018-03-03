using RpiFrame.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpiFrame
{
    public class MediaSequencer
    {
        public MediaSequencer()
        {
        }


        private IEnumerable<MediaFile> NextSequence { get; set; }

        private bool NextSequencePrepared => NextSequence != null;


        /// <summary>
        /// Prepares the next sequence by loading image files and doing some processing
        /// </summary>
        public void PrepareNextSequence(IEnumerable<MediaFileHeader> mediaHeaders) {
            NextSequence = null;
            NextSequence = mediaHeaders.Select(MediaLoader.LoadImage).ToList();
        }


        /// <summary>
        /// Fetches the next sequence when it's ready
        /// Prepare method has to be called in advance (manually)
        /// </summary>
        public async Task<IEnumerable<MediaFile>> GetNextSequence() {
            while (!NextSequencePrepared) {
                await Task.Delay(100);
            }

            return NextSequence;
        }


    }
}
