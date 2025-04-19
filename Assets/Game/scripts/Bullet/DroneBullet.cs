using Cysharp.Threading.Tasks;
using UnityEngine;

public class DroneBullet : Bullet
{
    [SerializeField] private int maxCount;

    private bool isEndedQueue = true;
    private int currentCount;
    

    public override async void Draw(BaseEnemy unit, Transform spawnPos)
    {
        await UniTask.WaitUntil(() => isEndedQueue);
        for (int i = 0; i < maxCount; i++)
        {
            isEndedQueue = false;
        float  heightK = 3f;
        GameObject prefabArrow = Instantiate(bullet, spawnPos.position, spawnPos.localRotation);
        Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
        CurrentArrow.SetDamage(Damage);
        Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * Speed;
        prefabArrow.transform.LookAt(unit.transform);
        await UniTask.DelayFrame(10);
        }

        isEndedQueue = true;

    }
}