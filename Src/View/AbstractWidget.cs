using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Strom.Btmvvm
{
    public abstract class AbstractWidget<TViewModel> : MonoBehaviour, IEventBindingContext
        where TViewModel : AbstractVmVariable, new()
    {
        [PublicAPI] public TViewModel ViewModel { get; private set; }

        private EvenBindingContextProvider _bindingContextProvider = default;
        private bool _initialized;
        
        private void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                OnInitialized();
            }
        }

        [PublicAPI]
        public void Bind(TViewModel viewModel)
        {
            Assert.IsTrue(viewModel != ViewModel,
                "Something wrong. Connected view model must be not equals reseted view model");

            Initialize();
            _bindingContextProvider?.CleanUp();
            ViewModel?.Dispose();
            ViewModel = viewModel;
            OnViewModelChanged(this);
        }
        
        public TBinding AddBinding<TBinding>(object bindingKey, TBinding binding) where TBinding : AbstractEventBinding
        {
            _bindingContextProvider ??= new EvenBindingContextProvider();
            return _bindingContextProvider.AddBinding(bindingKey, binding);
        }

        protected virtual void OnInitialized() {}
        protected abstract void OnViewModelChanged(IEventBindingContext ctx);
        protected virtual void OnDisposed() { }

        public void Dispose()
        {
            _bindingContextProvider?.CleanUp();
            ViewModel?.Unbind();
            ViewModel = default;
            OnDisposed();
        }
    }
}