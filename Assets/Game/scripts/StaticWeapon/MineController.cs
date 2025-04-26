using UnityEngine;

public class MineController : StaticWeaponController
{
    [SerializeField] private Mine minePrefab;
    [SerializeField] private float coolDown;
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;
    private float timer;

    protected override void UnLockedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= coolDown)
        {
            timer = 0f;
            Instantiate(minePrefab, randomPointInBoxCollider.GetRandomPointInBox(), Quaternion.identity);
        }
    }
}