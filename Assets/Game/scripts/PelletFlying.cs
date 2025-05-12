using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletFlying : MonoBehaviour
{
    private int _damage;
    private Vector2 _direction;
    [SerializeField] private Rigidbody2D _rigidbody;
    private Vector3 savedDirection;
    private float speed;
    private float timer;
    private bool canFly;

    public void Init(Vector2 direction, WeaponUpgradeData weaponUpgradeData)
    {
        _direction = direction;
        _damage = (int)weaponUpgradeData.GetStat(StatName.Damage).CurrentValue;
        speed = weaponUpgradeData.GetStat(StatName.ProjectileSpeed).CurrentValue;
        canFly = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);


        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {

            damageable.GetDamage(_damage);
        }

        Destroy(gameObject);
    }
    private void Update()
    {
        if(!canFly)
        {
            return;
        }
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _rigidbody.rotation = angle;
        _rigidbody.velocity = _direction.normalized * speed;
        savedDirection = _direction;

        timer += Time.deltaTime;
        if (timer >= 2f)
            Destroy(gameObject);

    }
}
