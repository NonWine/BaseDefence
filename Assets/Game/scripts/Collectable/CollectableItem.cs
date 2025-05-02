using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableItem : PoolAble
{


    public override void ResetPool()
    {
        gameObject.SetActive(true);
    }
}