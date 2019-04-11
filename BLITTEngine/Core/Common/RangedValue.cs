namespace BLITTEngine.Core.Common
{
    public class RangedValue
    {
        public float Value;

        public float StartValue { get; private set; }
        
        public float EndValue { get; private set; } 
        

        public RangedValue(float startValue, float endValue)
        {
            this.Value = startValue;
            this.StartValue = startValue;
            this.EndValue = endValue;
        }

        public void Swap()
        {
            var temp = StartValue;
            StartValue = EndValue;
            EndValue = temp;
        }
    }
}