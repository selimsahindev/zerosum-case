using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ObjectPool<T>
{
    private List<T> pool = new List<T>();

    public UnityAction<T> onPush;
    public UnityAction<T> onPop;
    public Func<T> CreateFunction;

    public ObjectPool(int size, Func<T> CreateFunction = null, UnityAction<T> onPush = null, UnityAction<T> onPop = null)
    {
        this.onPush = onPush;
        this.onPop = onPop;
        this.CreateFunction = CreateFunction;

        Populate(size);
    }

    private void Populate(int size)
    {
        // We create a sample to see if everything is ok.
        T testObject = CreateFunction();
        if (testObject == null)
        {
            return;
        }

        Push(testObject);

        for (int i = 1; i < size; i++)
        {
            Push(CreateFunction());
        }
    }

    public void Push(T item)
    {
        if (item == null) return;

        onPush?.Invoke(item);

        pool.Add(item);
    }

    public T Pop()
    {
        T itemToPop;

        if (pool.Count > 0)
        {
            itemToPop = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            // Cerate new instance if the pool is empty.
            itemToPop = CreateFunction();
        }

        onPop?.Invoke(itemToPop);
        return itemToPop;
    }
}