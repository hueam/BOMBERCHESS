using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolElement
{
    public PoolType type;
    public int cnt;
    public PoolableMono prefabs;
} 
[CreateAssetMenu(menuName = "SO/PoolList")]
public class PoolList : ScriptableObject
{
    public List<PoolElement> list;
}
