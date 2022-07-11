using TMPro;
using UnityEngine;

namespace Assets.Code.UI
{
    [ExecuteAlways]
    public class ProgressBar : MonoBehaviour
    {
        private const string _currentName = "{current}";
        private const string _maxName = "{max}";
        private const string _minName = "{min}";


        [SerializeField]
        private RectTransform _child;

        [SerializeField]
        private RectTransform _backGround;

        [SerializeField]
        private TextMeshProUGUI _header;

        [SerializeField]
        private float _minValue = 0f;

        public float MinValue
        {
            get => _minValue;
            set => ModifyAndAlert(ref _minValue, value);
        }
        [SerializeField]
        private float _currentValue;
        public float CurrentValue
        {
            get => _currentValue;
            set => ModifyAndAlert(ref _currentValue, value);
        }

        [SerializeField]
        public float _maxValue = 100f;
        public float MaxValue
        {
            get => _maxValue;
            set => ModifyAndAlert(ref _maxValue, value);
        }

        [SerializeField]
        public string _text = "";
        public string Text
        {
            get => _text;
            set => ModifyAndAlert(ref _text, value);
        }


        [SerializeField]
        private float _leftPadding = 0f;
        [SerializeField]
        private float _rightPadding = 0f;

        private void Awake()
        {
            if (_backGround == null)
                _backGround = transform.GetChild(0).GetComponent<RectTransform>();
            if (_child == null)
                _child = _backGround?.GetChild(0).GetComponent<RectTransform>();
        }

        private void Update()
        {
            StateHasChanged();
        }

        public void SetCurrentValue(float value)
        {
            if (value.Equals(value))
                return;

            _currentValue = value;
            StateHasChanged();
        }

        private void StateHasChanged()
        {
            SetProgressBar();

            SetHeaderText();
        }

        private void SetProgressBar()
        {
            float rangeWithPadding = _backGround.rect.width;
            if (rangeWithPadding < 0)
            {
                rangeWithPadding *= -1f;
            }
            float range = rangeWithPadding - (_leftPadding + _rightPadding);

            float progress = _maxValue <= _minValue ? 1f : (_currentValue - _minValue) / (_maxValue - _minValue);
            float invertedProgress = 1 - progress;

            float offset = range * invertedProgress;
            offset += _rightPadding;

            Vector2 newPosition = Vector2.left * offset;
            _child.anchoredPosition = newPosition;
        }
        private void SetHeaderText()
        {
            const string floatCulture = "0.0";
            string text = _text
                .Replace(_currentName, _currentValue.ToString(floatCulture))
                .Replace(_minName, _minValue.ToString(floatCulture))
                .Replace(_maxName, _maxValue.ToString(floatCulture));

            _header.text = text;
        }

        private void ModifyAndAlert<T>(ref T target, T value)
        {
            if (Equals(target, value))
                return;
            target = value;
            StateHasChanged();
        }
    }
}

