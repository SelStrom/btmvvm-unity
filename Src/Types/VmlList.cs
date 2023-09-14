using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Strom.Btmvvm
{
    public class VmlList<TElement> : IVmVariable, IList<TElement>
    {
        private Action<int, TElement> _onElementAdded;
        private Action<int, TElement> _onElementChanged;
        private Action<TElement> _onElementRemoved;
        private Action<VmlList<TElement>> _onContentChanged;

        [CanBeNull] private List<TElement> _list = new ();
        public IReadOnlyList<TElement> Value => _list;
        private bool _isBond;

        public VmlList() { }
        public VmlList(IReadOnlyCollection<TElement> initCollection) => GetList().AddRange(initCollection);

        private List<TElement> GetList() => _list ??= new List<TElement>();

        public void Bind(Action<VmlList<TElement>> onContentChanged,
            Action<int, TElement> onElementAdded,
            Action<int, TElement> onElementChanged,
            Action<TElement> onElementRemoved)
        {
            if (_isBond)
            {
                throw new InvalidOperationException("Already bond");
            }

            _onContentChanged = onContentChanged;
            _onElementAdded = onElementAdded;
            _onElementChanged = onElementChanged;
            _onElementRemoved = onElementRemoved;
            _isBond = true;

            if (_list != null)
            {
                _onContentChanged?.Invoke(this);
            }
        }

        public void Add(TElement item)
        {
            GetList().Add(item);
            _onElementAdded?.Invoke(_list.Count - 1, item);
        }

        public void Insert(int index, TElement item)
        {
            GetList().Insert(index, item);
            _onElementAdded?.Invoke(index, item);
        }

        public bool Remove(TElement item)
        {
            var removed = _list!.Remove(item);
            if (removed)
            {
                _onElementRemoved?.Invoke(item);
            }
            return removed;
        }

        public void RemoveAt(int index)
        {
            var item = _list![index];
            _list!.RemoveAt(index);
            _onElementRemoved?.Invoke(item);
        }

        public void Clear()
        {
            _list!.Clear();
            _onContentChanged?.Invoke(this);
        }

        public TElement this[int index]
        {
            get => _list![index];
            set
            {
                _list![index] = value;
                _onElementChanged?.Invoke(index, value);
            }
        }

        public int Count => _list!.Count;
        public bool Contains(TElement item) => _list!.Contains(item);
        public int IndexOf(TElement item) => _list!.IndexOf(item);

        public void CopyTo(TElement[] array, int arrayIndex) => _list!.CopyTo(array, arrayIndex);
        bool ICollection<TElement>.IsReadOnly => false;

        public void Unbind()
        {
            _onContentChanged = default;
            _onElementAdded = default;
            _onElementChanged = default;
            _onElementRemoved = default;
            _isBond = false;
        }

        void IVmVariable.Dispose()
        {
            Unbind();
            _list = default;
        }

        public IEnumerator<TElement> GetEnumerator() => _list!.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list)!.GetEnumerator();
    }
}