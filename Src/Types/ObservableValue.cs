
using System;
using System.Collections.Generic;

namespace Strom.Btmvvm
{
    public struct ObservableValue<T> : IObservableValue<T>, IEquatable<ObservableValue<T>>
    {
        public event Action<T> OnChanged;
        
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _value = value;
                    OnChanged?.Invoke(value);
                }
            }
        }

        public ObservableValue(T initial)
        {
            _value = initial;
            OnChanged = null;
        }

        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(_value);
        
        public bool Equals(ObservableValue<T> other) => EqualityComparer<T>.Default.Equals(_value, other._value);
        public override bool Equals(object obj) => obj is ObservableValue<T> other && Equals(other);

        public static bool operator ==(ObservableValue<T> left, ObservableValue<T> right) => left.Equals(right);
        public static bool operator !=(ObservableValue<T> left, ObservableValue<T> right) => !left.Equals(right);
    }
}