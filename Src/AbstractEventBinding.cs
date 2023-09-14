namespace Strom.Btmvvm
{
    public abstract class AbstractEventBinding
    {
        public abstract void Invoke();
        public abstract void Dispose();
    }

    public abstract class AbstractEventBinding<TBinding> : AbstractEventBinding where TBinding : AbstractEventBinding
    {
        public static TBinding Get() => BindingPool.Get<TBinding>();
    }
}