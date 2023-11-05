using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collection<T> : ScriptableObject
{
    [SerializeField] private T[] _items;
    private List<T> Items => new List<T>(_items);

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > _items.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return _items[index];
        }
    }

    public T RandomItem => _items[Random.Range(0, _items.Length)];
}
