namespace RpiFrame.Entities
{
    public class RangeSetting
    {
        public RangeSetting(double min, double max, bool enabled = true)
        {
            this.Enabled = enabled;
            this.Min = (float)min;
            this.Max = (float)max;
        }

        public RangeSetting(bool enabled = false)
        {
            this.Enabled = enabled;
            this.Min = 0;
            this.Max = 1f;
        }

        public bool Enabled { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
    }
}
