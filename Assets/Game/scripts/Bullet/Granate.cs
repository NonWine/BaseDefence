using UnityEngine;
public class Granate : Bullet
{
    

    public override void Draw(BaseEnemy unit, Transform posThrow)
    {
        // float heightK = 4f;
        // GameObject prefabArrow = Instantiate(bullet, posThrow.position, Player.Instance.gameObject.GetComponent<PlayerController>().GetBody().rotation);
        // Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
        // CurrentArrow.SetDamage((int)Attribute(ValueAttribute.Damage).Value);
        // prefabArrow.GetComponent<Explosive>().SetRadius(Attribute(ValueAttribute.Radius).Value);
        // prefabArrow.GetComponent<Explosive>().SetExploseDamage(Damage);
        // Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        // bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * Speed;
    }
    

}
