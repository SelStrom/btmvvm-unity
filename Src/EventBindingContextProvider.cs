using System;
using System.Collections.Generic;

namespace Strom.Btmvvm
{
    public class EvenBindingContextProvider
    {
        private readonly Dictionary<object, AbstractEventBinding> _keyToBinding = new();

        public TBinding AddBinding<TBinding>(object bindingKey, TBinding binding)
            where TBinding : AbstractEventBinding
        {
            if (_keyToBinding.ContainsKey(bindingKey))
            {
                throw new Exception("The binding has already exists");
            }

            _keyToBinding.Add(bindingKey, binding);
            return binding;
        }

        public void CleanUp()
        {
            foreach (var abstractEventBinding in _keyToBinding.Values)
            {
                abstractEventBinding.Dispose();
                BindingPool.Release(abstractEventBinding);
            }

            _keyToBinding.Clear();
        }
    }
}