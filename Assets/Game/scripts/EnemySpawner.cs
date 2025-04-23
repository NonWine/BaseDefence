using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;
    [Inject] private EnemyFactory enemyFactory;
    private WaveData _currentWave;
    private float _spawnTimer;
    private float _currentInterval;

    private bool _spawningEnabled;



    public int CurrentEnemies { get; private set; }

    public void StartSpawning(WaveData wave)
    {
        _currentWave = wave;
        _currentInterval = Random.Range(wave.MinSpawnInterval, wave.MaxSpawnInterval);
        _spawnTimer = 0;
        _spawningEnabled = true;
    }

    public void StopSpawning()
    {
        _spawningEnabled = false;
    }

    public void UpdateSpawner(float deltaTime)
    {
        if(CurrentEnemies >= 5) return;
        if (!_spawningEnabled) return;

        _spawnTimer += deltaTime;
        if (_spawnTimer >= _currentInterval)
        {
            SpawnRandomGroup();
            _spawnTimer = 0;
            _currentInterval = Random.Range(_currentWave.MinSpawnInterval, _currentWave.MaxSpawnInterval);
        }
    }

    public void TemporarilyIncreaseSpawnRate()
    {
        _currentInterval = Mathf.Max(_currentInterval * 0.5f, 0.5f);
    }

    public void SpawnExtraGroup()
    {
        SpawnRandomGroup();
    }

    private void SpawnRandomGroup()
    {
        var group = _currentWave.Enemies[Random.Range(0, _currentWave.Enemies.Count)];
        for (int i = 0; i < group.Count; i++)
        {
            if (group.SelectedEnemy is PoolAble poolAble)
            {
                var enemy = enemyFactory.Create(poolAble.Type);
                enemy.GetComponent<NavMeshAgent>().Warp(randomPointInBoxCollider.GetRandomPointInBox());
                CurrentEnemies++;
                
            }
        }
    }

    public void OnEnemyKilled()
    {
        CurrentEnemies = Mathf.Max(0, CurrentEnemies - 1);
    }
}