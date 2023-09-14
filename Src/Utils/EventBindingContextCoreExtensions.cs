using System;

namespace Strom.Btmvvm
{
    public static class EventBindingContextCoreExtensions
    {
        public static ObservableValueEventBinding<TSource, VmValue<TSource>> Bind<TSource>(
            this IEventBindingContext ctx,
            ref ObservableValue<TSource> field, //филд значимый, нужно обеспечить уникальность ключа
            VmValue<TSource> vmValue)
        {
            var binding = ObservableValueEventBinding<TSource, VmValue<TSource>>.Get();
            binding.Init(field, vmValue, (src, dest) => dest.Value = src);
            return ctx.AddBinding(field, binding);
        }

        public static ObservableValueEventBinding<TSource, TContext> Bind<TSource, TContext>(
            this IEventBindingContext ctx,
            ref ObservableValue<TSource> src,
            TContext context, 
            Action<TSource, TContext> action)
        {
            var binding = ObservableValueEventBinding<TSource, TContext>.Get();
            binding.Init(src, context, action);
            return ctx.AddBinding(src, binding);
        }

        public static ObservableValueEventBinding<TSource> Bind<TSource>(
            this IEventBindingContext ctx,
            ObservableValue<TSource> src,
            Action<TSource> action)
        {
            var binding = ObservableValueEventBinding<TSource>.Get();
            binding.Init(src, action);
            return ctx.AddBinding(src, binding);
        }
    }
}