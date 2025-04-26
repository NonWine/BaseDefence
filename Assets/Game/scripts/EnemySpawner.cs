using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;
    [SerializeField] private int _timeWhenStopSpawning;
    [Inject] private EnemyFactory enemyFactory;
    [Inject] WaveManager waveManager;
    private WaveData _currentWave;
    private float _spawnTimer;
    private float _currentInterval;

    private bool _spawningEnabled;



    public int CurrentEnemies { get; private set; }

    public void StartSpawning(WaveData wave)
    {
        _currentWave = wave;
        _currentInterval = wave.SpawnInterval;
        
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
        if(_spawnTimer >= 1f)
        {
            float evaluatedCurve = _currentWave.intervalCurve.Evaluate(1f -
                ((float)waveManager.CurrentTime) / ((float)_currentWave.waveDuration));
            _currentInterval = (float)_currentWave.SpawnInterval / evaluatedCurve;
        }
        if (_spawnTimer >= _currentInterval)
        {
            SpawnRandomGroup();
            _spawnTimer = 0;
            
                
        }
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
                if( waveManager.CurrentTime <= _timeWhenStopSpawning)
                {
                    StopSpawning();
                    break;
                }
            }
        }
    }

    public void OnEnemyKilled()
    {
        CurrentEnemies = Mathf.Max(0, CurrentEnemies - 1);
    }
}