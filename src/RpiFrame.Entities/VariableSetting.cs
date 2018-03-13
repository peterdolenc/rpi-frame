namespace RpiFrame.Entities
{
    public class VariableSetting
    {
        public VariableSetting(bool enabled, float amount)
        {
            this.Enabled = enabled;
            this.Amount = amount;
        }

        public VariableSetting(bool enabled, double amount)
        {
            this.Enabled = enabled;
            this.Amount = (float)amount;
        }

        public bool Enabled { get; set; }

        public float Amount { get; set; }
    }
}
