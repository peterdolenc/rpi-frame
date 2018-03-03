using System.Collections.Generic;

namespace RpiFrame
{
    public class MediaSequencer
    {
        public MediaSequencer()
        {
        }



        public List<MediaFile> GetNextSequence(List<MediaFile> mediaFiles) {

            // TODO: split to prepare + get
            // have a boolean indicating if it's ready yet
            // call prepare async
            // load binaries in preparation

            mediaFiles.ForEach(MediaLoader.LoadImageToPixbuf);
            return mediaFiles;
        }
    }
}
