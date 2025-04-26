using UnityEngine;

public class MineController : StaticWeaponController
{
    [SerializeField] private Mine minePrefab;
    [SerializeField] private float coolDown;
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;

    protected override void UnLockedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= coolDown)
        {
            timer = 0f;
         diContainer.InstantiatePrefabForComponent<Mine>(minePrefab, randomPointInBoxCollider.GetRandomPointInBox(), Quaternion.identity, null);
        }
    }
}