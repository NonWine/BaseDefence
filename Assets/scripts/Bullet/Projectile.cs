using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType myType;
    private int dmg;
    private float critChance = 20;
    private int critDmg = 20;
    private bool canCrit;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if(myType == ProjectileType.Granate)
        {
            transform.Rotate(Random.insideUnitSphere * 80f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Dire"))
        {
            if(myType == ProjectileType.Default && canCrit)
            {
                
                other.transform.GetComponent<BaseEnemy>().GetDamage(CritDamage());
            }
            else
            {
                if(myType != ProjectileType.Granate)
           //     other.transform.GetComponent<BaseEnemy>().GetDamage(dmg, dmg);
                if (myType == ProjectileType.Explosive || myType == ProjectileType.Granate)
                    GetComponent<Explosive>().Explose();
            }

            Destroy(gameObject);
        }
    }

    public void SetCrit() => canCrit = true;

    public void SetDamage(int value) => dmg = value;

    public void SetCritDamage(int value) => critDmg = value;

    public void SetCrtiChance(int value) => critChance = value;

    public int CritDamage()
    {
        float randvalue = Random.Range(0f, 101f);
        if(randvalue <= critChance)
        {
            ParticlePool.Instance.PlayExplosiveBullet(transform.position);
            dmg = dmg + critDmg;
        }

        return dmg;
    }

    
}
enum ProjectileType
{
    Default, Fire,Explosive,Frost,Granate
}
