using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<BaseEnemy>(out var baseEnemy))
        {
            Debug.Log("enemy touched the wall");
            if(baseEnemy.IsDeath)
            {
                return;
            }

        }
    }
}
