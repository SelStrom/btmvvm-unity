using System;

namespace Strom.Btmvvm
{
    public interface IObservableValue<out T>
    {
        event Action<T> OnChanged;
        T Value { get; }
    }
}