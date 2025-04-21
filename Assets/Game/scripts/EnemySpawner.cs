using UnityEngine;
using Zenject;

public class EnemySpawner
{
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
        var group = _currentWave.EnemyGroups[Random.Range(0, _currentWave.EnemyGroups.Count)];
        foreach (var enemy in group.Enemies)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                enemyFactory.Create(enemy.GetType());
                CurrentEnemies++;
            }
        }
    }

    public void OnEnemyKilled()
    {
        CurrentEnemies = Mathf.Max(0, CurrentEnemies - 1);
    }
}