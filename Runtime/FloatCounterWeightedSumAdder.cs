using System;
using System.Collections.Generic;
using UnityEngine;

namespace Counters
{
    public class FloatCounterWeightedSumAdder : MonoBehaviour
    {
        [SerializeField] private List<WeightedSource> _sources;
        [SerializeField] private FloatCounter _targetCounter;

        private Dictionary<WeightedSource, Action<float, float>> _subscriptions = new();
        private float _currentResult;

        private void OnEnable()
        {
            _currentResult = 0f;

            foreach (WeightedSource source in _sources)
            {
                Subscribe(source);
                OnSourceCounterChanged(source.Weight, 0f, source.Source.Count);
            }
        }

        private void OnDisable()
        {
            foreach (KeyValuePair<WeightedSource, Action<float, float>> keyValuePair in _subscriptions)
                keyValuePair.Key.Source.CountChanged -= keyValuePair.Value;

            _subscriptions.Clear();
            _targetCounter.Subtract(_currentResult);
            _currentResult = 0f;
        }

        public void AddSource(WeightedSource source)
        {
            _sources.Add(source);

            if (enabled == false)
                return;

            Subscribe(source);
            float delta = source.Source.Count * source.Weight;
            _targetCounter.Add(delta);
            _currentResult += delta;
        }

        public void RemoveSource(WeightedSource source)
        {
            if (_sources.Remove(source) == false)
                return;

            if (enabled == false)
                return;

            Unsubscribe(source);
            float delta = source.Source.Count * source.Weight;
            _targetCounter.Subtract(delta);
            _currentResult -= delta;
        }

        private void Subscribe(WeightedSource source)
        {
            Action<float, float> handler = (oldValue, newValue) => OnSourceCounterChanged(source.Weight, oldValue, newValue);
            _subscriptions[source] = handler;
            source.Source.CountChanged += handler;
        }

        private void Unsubscribe(WeightedSource source)
        {
            if (_subscriptions.TryGetValue(source, out Action<float, float> handler))
            {
                source.Source.CountChanged -= handler;
                _subscriptions.Remove(source);
            }
        }

        private void OnSourceCounterChanged(float weight, float oldValue, float newValue)
        {
            float delta = (newValue - oldValue) * weight;
            _targetCounter.Add(delta);
            _currentResult += delta;
        }
    }

    [Serializable]
    public class WeightedSource
    {
        [SerializeField] private FloatCounter _source;
        [SerializeField] private float _weight = 1f;

        public FloatCounter Source => _source;
        public float Weight => _weight;
    }
}