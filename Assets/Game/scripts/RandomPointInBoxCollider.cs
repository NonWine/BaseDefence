using UnityEngine;

public class RandomPointInBoxCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    // Возвращает случайную точку внутри BoxCollider
    public Vector3 GetRandomPointInBox()
    {
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider не назначен!");
            return Vector3.zero;
        }

        // Получаем размеры коллайдера
        Vector3 size = boxCollider.size;
        Vector3 center = boxCollider.center;

        // Получаем случайную точку в локальных координатах коллайдера
        Vector3 localRandomPoint = new Vector3(
            Random.Range(-size.x / 2f, size.x / 2f),
            Random.Range(-size.y / 2f, size.y / 2f),
            Random.Range(-size.z / 2f, size.z / 2f)
        );

        // Смещаем с учётом центра
        localRandomPoint += center;

        // Переводим в мировые координаты
        Vector3 worldPoint = boxCollider.transform.TransformPoint(localRandomPoint);

        return worldPoint;
    }

    // Пример вызова (можно удалить)
    private void Start()
    {
        Vector3 point = GetRandomPointInBox();
        Debug.Log("Случайная точка в BoxCollider: " + point);
    }
}