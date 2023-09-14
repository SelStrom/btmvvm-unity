using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Strom.Btmvvm
{
    [NotNull]
    public class VmValue<T> : IVmVariable
    {
        private T _value;

        private Action<T> _onChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _value = value;
                    _onChanged?.Invoke(value);
                }
            }
        }

        public VmValue(T initValue)
        {
            _value = initValue;
        }

        public VmValue() { }

        public void Bind(Action<T> onChanged)
        {
            var bound = _onChanged != null;
            if (bound)
            {
                throw new InvalidOperationException("Already bound");
            }

            _onChanged = onChanged;
            if (typeof(T).IsValueType || !EqualityComparer<T>.Default.Equals(_value, default(T)))
            {
                _onChanged.Invoke(Value);
            }
        }

        public void Unbind()
        {
            _onChanged = default;
        }

        void IVmVariable.Dispose()
        {
            Unbind();
            _value = default;
        }

        public static implicit operator T(VmValue<T> vmValue) => vmValue != null ? vmValue.Value : default;
        public static explicit operator VmValue<T>(T value) => value == null ? new VmValue<T>() : new VmValue<T>(value);

        public override string ToString() => $"[{_value?.ToString()}]";
    }
}