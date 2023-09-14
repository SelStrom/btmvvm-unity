using System;
using UnityEngine.UI;

namespace Strom.Btmvvm
{
    public static class EventBindingContextUiExtensions
    {
        public static void Bind(this IEventBindingContext ctx, Button button, Action onButtonClicked)
        {
            var binding = ButtonEventBindingSimple.Get();
            binding.Init(button, onButtonClicked);
            ctx.AddBinding(binding.Key, binding);
        }

        public static void Bind<TActionContext>(this IEventBindingContext ctx, Button button,
            TActionContext actionContext, Action<TActionContext> onButtonClicked)
        {
            var binding = ButtonEventBinding<TActionContext>.Get();
            binding.Init(button, onButtonClicked);
            binding.SetContext(actionContext);
            ctx.AddBinding(binding.Key, binding);
        }

        public static void Bind(this IEventBindingContext ctx, Button button,
            VmValue<Action> mvAction)
        {
            var binding = ButtonEventBinding<Action>.Get();
            binding.Init(button, action => action?.Invoke());
            mvAction.Bind(binding.OnActionValueChanged);
            ctx.AddBinding(binding.Key, binding);
        }
    }
}