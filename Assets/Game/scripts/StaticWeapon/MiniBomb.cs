using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBomb : MonoBehaviour
{
    [SerializeField] GameObject bombObj;
    [SerializeField] float time;
    [SerializeField]private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!bombObj.activeInHierarchy)
        {
            return;
        }
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {

            damageable.GetDamage(damage);
        }
        bombObj.SetActive(false);
        Invoke("EnablingBomb", time);
    }
    private void EnablingBomb()
    {
        bombObj.SetActive(true);
    }

}
