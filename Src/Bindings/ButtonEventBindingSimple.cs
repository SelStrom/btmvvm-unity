using System;
using UnityEngine.UI;

namespace Strom.Btmvvm
{
internal class ButtonEventBindingSimple : AbstractEventBinding<ButtonEventBindingSimple>
{
    private Action _buttonClickHandler;
    private Button _button;

    public string Key => $"{_button.name}";
    
    public ButtonEventBindingSimple Init(Button button, Action buttonClickHandler)
    {
        _button = button;
        _buttonClickHandler = buttonClickHandler;
        
        _button.onClick.AddListener(ClickHandler);
        return this;
    }
    
    public override void Invoke()
    {
        //
    }

    private void ClickHandler() => _buttonClickHandler?.Invoke();

    public override void Dispose()
    {
        _button.onClick.RemoveListener(ClickHandler);
        
        _button = null;
        _buttonClickHandler = null;
    }
}
}