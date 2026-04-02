using System;
using UnityEngine;

namespace Counters
{
    public abstract class Counter<TValue> : MonoBehaviour where TValue : struct, IComparable<TValue>
    {
        [SerializeField] private TValue _count;

        public event Action<TValue, TValue> CountChanged;

        public TValue Count => _count;

        public abstract TValue IncrementStep { get; }
        public abstract TValue ZeroValue { get; }

        public void Increment()
        {
            Add(IncrementStep);
        }

        public void Decrement()
        {
            Subtract(IncrementStep);
        }

        public void Add(TValue value)
        {
            TrySet(AddValues(Count, value));
        }

        public void Subtract(TValue value)
        {
            TrySet(SubtractValues(Count, value));
        }

        public void Set(TValue newCount)
        {
            TrySet(newCount);
        }

        protected abstract TValue AddValues(TValue leftValue, TValue rightValue);
        protected abstract TValue SubtractValues(TValue leftValue, TValue rightValue);

        private void TrySet(TValue newCount)
        {
            if (newCount.CompareTo(ZeroValue) < 0)
                return;

            if (Count.Equals(newCount))
                return;

            TValue oldCount = Count;
            _count = newCount;
            CountChanged?.Invoke(oldCount, newCount);
        }
    }
}