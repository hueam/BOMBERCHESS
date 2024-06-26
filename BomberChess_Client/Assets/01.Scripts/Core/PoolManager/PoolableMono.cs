using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour
{
    public PoolType type;
    public abstract void Init();
}
