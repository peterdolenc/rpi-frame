using System;
using System.Drawing;

namespace RpiFrame.Rendering
{
    public class RenderingProperties
    {
        public RenderingProperties()
        {
        }

        public Bitmap Image { get; set; }

        public Fitment Fitment { get; set; }

        public int Width => Image.Width;

        public int Height => Image.Height;

        public int CurrentStep { get; set; } = 0;

        public int MaxSteps { get; set; }

        public bool ScrollingDone => Fitment == Fitment.Still || CurrentStep >= MaxSteps;

        public int StepsLeft => MaxSteps - CurrentStep;
    }
}
