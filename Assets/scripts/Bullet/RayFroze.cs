using UnityEngine;
public class RayFroze : Bullet
{
   

    public override void Draw(BaseEnemy unit, Transform posThrow)
    {
        float heightK;
        heightK = 4f;
       // GameObject prefabArrow = Instantiate(bullet, posThrow.position, Player.Instance.gameObject.GetComponent<PlayerController>().GetBody().rotation);
       //  Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
       //  prefabArrow.GetComponent<Explosive>().SetFrostTime(Attribute(ValueAttribute.FrostTime).Value);
       //  prefabArrow.GetComponent<Explosive>().SetExploseDamage(Mathf.CeilToInt(Damage / 1.5f));
       //  CurrentArrow.SetDamage(Damage);
       //  CurrentArrow.GetComponent<Projectile>().SetCrit();
       //  Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
       //  bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * Speed;
    }


    
}
