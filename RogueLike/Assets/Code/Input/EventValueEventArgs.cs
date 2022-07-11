using System;

namespace Assets.Code.Input
{
    public class EventValueEventArgs<T> : EventArgs
    {
        public T NewValue { get; set; }
        public T OldValue { get; set; }

        public bool FromDefaultToDefined => IsDefault(OldValue) && !IsDefault(NewValue);
        public bool FromDefinedToDefault => !IsDefault(OldValue) && IsDefault(NewValue);
        public bool OldAndNewDiffer => Equals(NewValue, OldValue);

        private static bool IsDefault(T value) => Equals(value, default(T));
    }
}