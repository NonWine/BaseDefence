using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public event Action OnAttack;

    public void OnAttackFunc() => OnAttack?.Invoke();

    private void Awake()
    {
        OnAttack = null;
    }
}
