using UnityEngine;

public class MineController : MonoBehaviour
{
    [SerializeField] private Mine minePrefab;
    [SerializeField] private float coolDown;
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;
    private float timer;
    
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= coolDown)
        {
            timer = 0f;
            Instantiate(minePrefab, randomPointInBoxCollider.GetRandomPointInBox(), Quaternion.identity);
        }
    }
}