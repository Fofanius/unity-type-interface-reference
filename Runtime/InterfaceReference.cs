using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fofanius.Type
{
    [Serializable]
    public class InterfaceReference<T> where T : class
    {
        [SerializeField] private Object _object;

        public bool HasValue => _object;
        public T Value => _object as T;
        public Object BackingSource => _object;

        public void SetBackingSource(Object source)
        {
            _object = source;
        }

        public override string ToString() => $"({typeof(T).Name}) {(HasValue ? _object : "NULL")}";
    }
}