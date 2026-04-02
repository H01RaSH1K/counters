using System.Collections.Generic;
using UnityEngine;

namespace Counters
{
    public class FloatCounterProductAdder : MonoBehaviour
    {
        [SerializeField] private List<FloatCounter> _sourceCounters;
        [SerializeField] private FloatCounter _productCounter;

        private float _currentResult;

        private void OnEnable()
        {
            foreach (var counter in _sourceCounters)
                counter.CountChanged += OnSourceCounterChanged;

            _currentResult = 0f;
            Recalculate();
        }

        private void OnDisable()
        {
            foreach (var counter in _sourceCounters)
                counter.CountChanged -= OnSourceCounterChanged;

            _productCounter.Subtract(_currentResult);
        }

        public void AddSourceCounter(FloatCounter counter)
        {
            _sourceCounters.Add(counter);

            if (enabled == false)
                return;

            counter.CountChanged += OnSourceCounterChanged;
            Recalculate();
        }

        public void RemoveSourceCounter(FloatCounter counter)
        {
            if (_sourceCounters.Remove(counter) == false)
                return;

            if (enabled == false)
                return;

            counter.CountChanged -= OnSourceCounterChanged;
            Recalculate();
        }

        private void OnSourceCounterChanged(float oldValue, float newValue)
        {
            Recalculate();
        }

        private void Recalculate()
        {
            float newResult = 0f;

            if (_sourceCounters.Count > 0)
            {
                newResult = 1f;

                foreach (var counter in _sourceCounters)
                    newResult *= counter.Count;
            }

            float delta = newResult - _currentResult;
            _currentResult = newResult;

            _productCounter.Add(delta);
        }
    }
}