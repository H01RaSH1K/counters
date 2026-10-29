using UnityEngine;

namespace Anhertis.Counters
{
    [RequireComponent(typeof(FloatCounter))]
    public class PositiveDeltaAccumulator : MonoBehaviour
    {
        [SerializeField] private FloatCounter _sourceCounter;
        [SerializeField] private FloatCounter _targetCounter;

        private void OnEnable()
        {
            _sourceCounter.CountChanged += OnSourceCountChanged;
        }

        private void OnDisable()
        {
            _sourceCounter.CountChanged -= OnSourceCountChanged;
        }

        private void OnSourceCountChanged(float oldValue, float newValue)
        {
            float delta = newValue - oldValue;

            if (delta > 0f)
                _targetCounter.Add(delta);
        }
    }
}