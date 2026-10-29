namespace Anhertis.Counters
{
    public sealed class FloatCounter : Counter<float>
    {
        public override float IncrementStep => 1f;
        public override float ZeroValue => 0f;

        protected override float AddValues(float leftValue, float rightValue)
        {
            return leftValue + rightValue;
        }

        protected override float SubtractValues(float leftValue, float rightValue)
        {
            return leftValue - rightValue;
        }
    }
}