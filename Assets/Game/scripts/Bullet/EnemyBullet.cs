using System.Collections;
using System;
using UnityEngine;

public class EnemyBullet : BaseBullet
{
    public override Type Type => typeof(EnemyBullet);
    protected override void Update()
    {
        if (isAlive)
        {
            var dir = new Vector3(transform.position.x, _target.transform.position.y, _target.transform.position.z) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rigidbody.rotation = angle;
            rigidbody.velocity = dir.normalized * 100f;
            savedDirection = dir;

            if (_damageable.IsDeath)
            {
                isAlive = false;
                rigidbody.freezeRotation = true;
                rigidbody.velocity = savedDirection.normalized * 100f;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
                DestroyBullet();
        }
    }
}
