using TMPro;
using UnityEngine;

namespace Anhertis.Counters
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class CounterView<TValue> : MonoBehaviour where TValue : struct, System.IComparable<TValue>
    {
        protected static readonly string[] s_Suffixes =
        {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No",
        "Dc", "Ud", "Dd", "Td", "Qad", "Qid", "Sxd", "Spd", "Ocd", "Nod",
        "Vg", "Uvg", "Dvg", "Tvg", "Qavg", "Qivg", "Sxvg", "Spvg", "Ocvg"
    };

        protected static readonly char s_exponentChar = 'e';

        protected const int SuffixDigitsStep = 3;

        [SerializeField] private Counter<TValue> _counter;
        [SerializeField] protected string NumberFormat;

        protected TextMeshProUGUI TextComponent { get; private set; }

        private void Awake()
        {
            TextComponent = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _counter.CountChanged += OnCountChanged;
            UpdateText(_counter.Count);
        }

        private void OnDisable()
        {
            _counter.CountChanged -= OnCountChanged;
        }

        private void UpdateText(TValue newCount)
        {
            TextComponent.text = FormatValue(newCount);
        }

        private void OnCountChanged(TValue _, TValue newCount)
        {
            UpdateText(newCount);
        }

        protected abstract string FormatValue(TValue newCount);
    }
}