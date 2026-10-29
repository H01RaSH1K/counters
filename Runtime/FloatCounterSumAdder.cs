using System.Collections.Generic;
using UnityEngine;

namespace Anhertis.Counters
{
    public class FloatCounterSumAdder : MonoBehaviour
    {
        [SerializeField] private List<FloatCounter> _sourceCounters;
        [SerializeField] private FloatCounter _sumCounter;

        private void OnEnable()
        {
            foreach (FloatCounter counter in _sourceCounters)
            {
                OnSourceCounterChanged(0f, counter.Count);
                counter.CountChanged += OnSourceCounterChanged;
            }
        }

        private void OnDisable()
        {
            foreach (FloatCounter counter in _sourceCounters)
            {
                OnSourceCounterChanged(counter.Count, 0f);
                counter.CountChanged -= OnSourceCounterChanged;
            }
        }

        public void AddSourceCounter(FloatCounter counter)
        {
            if (enabled == false)
                return;

            OnSourceCounterChanged(0f, counter.Count);
            counter.CountChanged += OnSourceCounterChanged;
        }

        public void RemoveSourceCounter(FloatCounter counter)
        {
            if (_sourceCounters.Remove(counter))
            {
                if (enabled)
                {
                    OnSourceCounterChanged(counter.Count, 0f);
                    counter.CountChanged -= OnSourceCounterChanged;
                }
            }
        }

        private void OnSourceCounterChanged(float oldValue, float newValue)
        {
            _sumCounter.Add(newValue - oldValue);
        }
    }
}