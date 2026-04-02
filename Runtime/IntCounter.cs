namespace Counters
{
    public sealed class IntCounter : Counter<int>
    {
        public override int IncrementStep => 1;
        public override int ZeroValue => 0;

        protected override int AddValues(int leftValue, int rightValue)
        {
            return leftValue + rightValue;
        }

        protected override int SubtractValues(int leftValue, int rightValue)
        {
            return leftValue - rightValue;
        }
    }
}