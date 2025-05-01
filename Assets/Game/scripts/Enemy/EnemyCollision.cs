using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] BaseEnemy _baseEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Target>(out var target))
        {
            _baseEnemy.Attack();
        }
        if(other.CompareTag("StinkyCloud"))
        {
            _baseEnemy.IsPoisoned = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("StinkyCloud"))
        {
            Debug.Log("exit stinky cloud");
            _baseEnemy.IsPoisoned = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Target>(out var target))
        {
            _baseEnemy.Attack();
        }
    }
}
