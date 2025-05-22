using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provolka : StaticWeaponObj
{
    private List<BaseEnemy> enemiesInCollider;
    private float damageInterval;
    private float timer;

    private void Start()
    {
        enemiesInCollider = new List<BaseEnemy>();
        damageInterval = WeaponUpgradeData.GetStat(StatName.DamageInterval).CurrentValue;
        Debug.Log("damage interval is " + damageInterval);
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
            if (enemiesInCollider.Count>0)
            {          
                for(int i = 0; i < enemiesInCollider.Count; i++)
                {
                    if (!enemiesInCollider[i].gameObject.activeInHierarchy)
                    {
                        enemiesInCollider.RemoveAt(i);
                        continue;
                    }
                    enemiesInCollider[i].GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValue);
                }
            }
            timer = 0;
        }
    }
}
