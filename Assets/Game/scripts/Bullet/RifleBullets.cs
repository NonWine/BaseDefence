using UnityEngine;

public class RifleBullets : Bullet
{
    [SerializeField] private Transform[] riflePosThrow;
    

    public override void Draw(BaseEnemy unit, Transform posthrow)
    {
        float heightK;
        heightK = 1f;
        float offset = -2f;
        for (int i = 0; i < riflePosThrow.Length; i++)
        {
            GameObject prefabArrow = Instantiate(bullet, riflePosThrow[i].position + new Vector3(0f,heightK,0f), Quaternion.identity);
            Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
            CurrentArrow.SetDamage(Damage);
            Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
            bulletBody.velocity = riflePosThrow[i].forward  * Speed;
            offset += 1f;
        }
    }

    
}
