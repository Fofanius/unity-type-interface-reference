using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fofanius.Type
{
    [Serializable]
    public class InterfaceReference<T> where T : class
    {
        [SerializeField] private Object _object;

        public bool IsEmpty => !_object;
        public bool HasValue => _object;

        public void SetSource(Object source)
        {
            _object = source;
        }

        public T GetValue()
        {
            return _object switch
            {
                Component component => component as T,
                ScriptableObject scriptableObject => scriptableObject as T,
                _ => default
            };
        }

        public override string ToString() => $"({typeof(T).Name}) {(IsEmpty ? "NULL" : _object)}";
    }
}