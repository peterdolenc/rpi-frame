namespace RpiFrame.Interfaces
{
    public interface IImageWindow
    {
        void LoadImage(byte[] buffer);
        void Scroll(int horizontalAdvancement, int verticalAdvancement);
    }
}
