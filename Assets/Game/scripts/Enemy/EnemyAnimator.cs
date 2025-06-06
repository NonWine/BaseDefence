﻿using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] public Animator _animator;

    public Animator Animator => _animator;
    public void SetAnimator(Animator animator) => _animator = animator;

    public AnimationEvent AnimationEvent => _animator.GetComponent<AnimationEvent>();
    
    public void SetRun()
    {
        _animator.SetInteger("State",0);

        _animator.SetFloat("Speed",1);
    }
    
    public void SetAttack()
    {
        _animator.SetFloat("Speed",-1);

        _animator.SetInteger("State",1);
    }


    public void SetIdle()
    {
        _animator.SetFloat("Speed",-1);

        _animator.SetInteger("State",0);
    }


    public void SetDie()
    {
        _animator.SetFloat("Speed",-1);
        _animator.SetInteger("State",3);

    }
    public void Freeze()
    {
        _animator.speed = 0;
    }
    public void UnFreeze()
    {
        _animator.speed = 1;
    }
}