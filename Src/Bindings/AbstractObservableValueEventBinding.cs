using System;

namespace Strom.Btmvvm
{
    public class ObservableValueEventBinding<TSrc, TContext> : AbstractEventBinding<ObservableValueEventBinding<TSrc, TContext>>
    {
        private ObservableValue<TSrc> _src;
        private TContext _context;
        private Action<TSrc, TContext> _callBack;

        public ObservableValueEventBinding<TSrc, TContext> Init(ObservableValue<TSrc> src, TContext context,
            Action<TSrc, TContext> callBack)
        {
            _src = src;
            _context = context;
            _callBack = callBack;
            
            _src.OnChanged += OnSrcChanged;
            return this;
        }

        public override void Invoke()
        {
            OnSrcChanged(_src.Value);
        }

        private void OnSrcChanged(TSrc src)
        {
            _callBack?.Invoke(src, _context);
        }

        public override void Dispose()
        {
            _src.OnChanged -= OnSrcChanged;
            
            _src = default;
            _context = default;
            _callBack = null;
        }
    }

    public class ObservableValueEventBinding<TSrc> : AbstractEventBinding<ObservableValueEventBinding<TSrc>>
    {
        private ObservableValue<TSrc> _src;
        private Action<TSrc> _callBack;

        public ObservableValueEventBinding<TSrc> Init(ObservableValue<TSrc> src, Action<TSrc> callBack)
        {
            _src = src;
            _callBack = callBack;
            
            _src.OnChanged += OnSrcChanged;
            return this;
        }

        public override void Invoke()
        {
            OnSrcChanged(default);
        }

        private void OnSrcChanged(TSrc _)
        {
            _callBack?.Invoke(_src.Value);
        }

        public override void Dispose()
        {
            _src.OnChanged -= OnSrcChanged;

            _src = default;
            _callBack = null;
        }
    }
}