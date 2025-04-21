using UnityEngine;

public class Explosive : MonoBehaviour
{

    [SerializeField] private ExplosiveType myType;
    [SerializeField] private float _radius;
    [SerializeField] private int damageExplose;
    [SerializeField] private float frozeTime;

    public void Explose()
    {

        if (myType == ExplosiveType.Explose)
            ParticlePool.Instance.PlayExplossion(transform.position,_radius);
        else if (myType == ExplosiveType.ExplosiveBullet)
            ParticlePool.Instance.PlayExplosiveBullet(transform.position);
        else if (myType == ExplosiveType.Frozen)
        {
            ParticlePool.Instance.PlayFrozenExplose(transform.position);
        }
           
        Collider[] enemies =  Physics.OverlapSphere(transform.position, _radius);
        foreach (var item in enemies)
        {
            if (item.CompareTag("Dire"))
            {
                BaseEnemy enemy = item.GetComponent<BaseEnemy>();

                 enemy.GetDamage(damageExplose);
                if (myType == ExplosiveType.Frozen)
                {

                }
                  
            }
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _radius);
    }

    public void SetExploseDamage(int value) => damageExplose = value;
    public void SetFrostTime(float value)
    {
        frozeTime = value;
    }

    public void SetRadius(float value) => _radius = value;
}

public enum ExplosiveType {Explose, Frozen,ExplosiveBullet }