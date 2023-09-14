using System;
using UnityEngine.UI;

namespace Strom.Btmvvm
{
    public class ButtonEventBinding<TContext> : AbstractEventBinding<ButtonEventBinding<TContext>>
    {
        private Action<TContext> _buttonClickHandler;
        private TContext _context;
        private Button _button;

        public string Key => $"{_button.name}";

        public ButtonEventBinding<TContext> Init(Button button,
            Action<TContext> buttonClickHandler)
        {
            _button = button;
            _buttonClickHandler = buttonClickHandler;

            _button.onClick.AddListener(ClickHandler);
            return this;
        }

        public ButtonEventBinding<TContext> SetContext(TContext context)
        {
            _context = context;
            return this;
        }
        
        internal void OnActionValueChanged(TContext context)
        {
            var _ = SetContext(context);
        }

        public override void Invoke()
        {
            //
        }

        private void ClickHandler() => _buttonClickHandler?.Invoke(_context);

        public override void Dispose()
        {
            _button.onClick.RemoveListener(ClickHandler);
            
            _context = default(TContext);
            _button = null;
            _buttonClickHandler = null;
        }
    }
}