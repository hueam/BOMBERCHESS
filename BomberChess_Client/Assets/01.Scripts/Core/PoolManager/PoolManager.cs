using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PoolType
{
    RoomElement,

}
public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<PoolType, Pool<PoolableMono>> typeByPool = new();
    public void Init(PoolList list)
    {
        foreach (var item in list.list)
        {
            if(typeByPool.ContainsKey(item.type)) Debug.LogError($"Already have {item.type.ToString()}Pool");
            typeByPool.Add(item.type, new Pool<PoolableMono>(item.prefabs, transform, item.cnt));
        }
    }

    public PoolableMono Pop(PoolType type)
    {
        if (!typeByPool.ContainsKey(type))
        {
            Debug.LogError($"Don't have {type.ToString()}type Pool");
            return null;
        }
        return typeByPool[type].Pop();
    }

    public void Push(PoolableMono obj)
    {
        typeByPool[obj.type].Push(obj);
    }
}
