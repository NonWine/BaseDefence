using System;
using UnityEngine;

public abstract class PoolAble : MonoBehaviour
{
    public abstract void ResetPool();
    
    public abstract Type Type { get; }
    
}