using System.Drawing;
using System.IO;

namespace RpiFrame.Rendering
{
    public static class ImageConverter
    {
        public static Bitmap ToBitmap(this byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return new Bitmap(ms);
            }
        }

        public static byte[] ToImageBytes(this Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                return stream.ToArray();
            }
        }


    }
}
