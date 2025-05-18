using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provolka : MonoBehaviour
{
    private List<BaseEnemy> enemiesInCollider;
    [SerializeField] float damageInterval;
    private float timer;

    private void Start()
    {
        enemiesInCollider = new List<BaseEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.TryGetComponent<BaseEnemy>(out var enemy))
        {
            enemiesInCollider.Add(enemy);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<BaseEnemy>(out var enemy))
        {
            enemiesInCollider.Remove(enemy);

        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > damageInterval)
        {
            if (enemiesInCollider != null)
            {
                /*foreach (var enemy in enemiesInCollider)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        enemiesInCollider.Remove(enemy);
                    }
                    enemy.GetDamage(10);

                }*/
                for(int i = 0; i < enemiesInCollider.Count; i++)
                {
                    if (!enemiesInCollider[i].gameObject.activeInHierarchy)
                    {
                        enemiesInCollider.RemoveAt(i);
                    }
                    enemiesInCollider[i].GetDamage(10);
                }
            }
            
        }
    }
}
