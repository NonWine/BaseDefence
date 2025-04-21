using Sirenix.OdinInspector;

[System.Serializable]
public class EnemySpawnInfo
{
   [ShowInInspector] public BaseEnemy EnemyType;
    public int Count;
    public float SpawnDelay;
}