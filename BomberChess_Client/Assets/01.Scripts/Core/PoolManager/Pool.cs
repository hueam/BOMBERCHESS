using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Queue<T> queue = new();
    private T prefab;
    private Transform parent;
    public Pool(T prefab, Transform parent, int cnt = 10)
    {
        this.parent = parent;
        this.prefab = prefab;
        for (int i = 0; i < cnt; i++)
        {
            T instance = GameObject.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            queue.Enqueue(instance);
        }
    }
    public T Pop()
    {
        T obj = null;
        if (queue.Count <= 0)
        {
            obj = GameObject.Instantiate(prefab);
        }
        else
        {
            obj = queue.Dequeue();
        }
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        queue.Enqueue(obj);
    }
}
