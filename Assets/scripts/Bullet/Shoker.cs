using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoker : Bullet
{
    [SerializeField] private int maxRc;
    [SerializeField] private int _radius;

 

    public override void Draw(BaseEnemy unit, Transform posThrow)
    {
        // float heightK;
        // heightK = 4f;
        // GameObject prefabArrow = Instantiate(bullet, posThrow.position, Player.Instance.gameObject.GetComponent<PlayerController>().GetBody().rotation);
        // ShokerTile CurrentArrow = prefabArrow.GetComponent<ShokerTile>();
        // CurrentArrow.SetDamage(Damage);
        // CurrentArrow.SetMaxRc(maxRc);
        // CurrentArrow.SetRadius(_radius);
        // CurrentArrow.SetSpeed(Speed);
        // Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        // bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * Speed;

    }

}
