using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAgrRadius : MonoBehaviour
{

    [field: SerializeField]    public Transform CurrentAgredTransform { get; private set; }

    private IDamageable _lastDamageable;

    public Queue<Transform> Targets = new Queue<Transform>();

    public Team Team { get; set; }

    private void OnDisable()
    {
        Targets.Clear();
        _lastDamageable = null;
        CurrentAgredTransform = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {   
            if(damageable.Team != Team)
                Targets.Enqueue(other.transform);
            
        }
    }

    public bool TryGetTargetDamageable(out IDamageable targetDamageable)
    {
        targetDamageable = _lastDamageable;
        
        if (targetDamageable?.IsDeath == false && targetDamageable.Team != Team)
        {
            return true;
        }
        
        while (Targets.Count > 0)
        {
            Transform targetTransform = Targets.Peek();
            
            if (targetTransform.TryGetComponent(out IDamageable damageable))
            {
                if (!damageable.IsDeath && damageable.Team != Team)
                {
                    targetDamageable = damageable;
                    _lastDamageable = targetDamageable;
                    CurrentAgredTransform = targetTransform;
                    return true;
                }
            } 
            Targets.Dequeue();
        }

        return false;
    }

    
}

