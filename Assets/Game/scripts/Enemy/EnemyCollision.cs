using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] BaseEnemy _baseEnemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Target>(out var target))
        {
            _baseEnemy.EnemyStateMachine.ChangeState<AttackState>();
            _baseEnemy.Speed = 0;
        }
        if(other.CompareTag("StinkyCloud"))
        {
            _baseEnemy.IsPoisoned = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("StinkyCloud"))
        {
            _baseEnemy.IsPoisoned = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Target>(out var target))
        {
            _baseEnemy.EnemyStateMachine.ChangeState<AttackState>();
            _baseEnemy.Speed = 0;
        }
    }
}
