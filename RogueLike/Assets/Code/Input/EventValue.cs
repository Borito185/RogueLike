using System;
using UnityEngine;

namespace Assets.Code.Input
{
    [Serializable]
    public class EventValue<TValue>
    {
        [SerializeField]
        private TValue _value;
        public TValue Value
        {
            get => _value;
            set => SetValue(value);
        }

        public event EventHandler<EventValueEventArgs<TValue>> OnValueSet;

        private void SetValue(TValue newValue)
        {
            TValue oldValue = _value;
            _value = newValue;

            OnValueSet?.Invoke(this, new() {OldValue = oldValue, NewValue = newValue});
        }
    }
}
