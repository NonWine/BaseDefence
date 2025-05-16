using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;
    [SerializeField] private int _timeWhenStopSpawning;
    [Inject] private EnemyFactory enemyFactory;
    [Inject] WaveManager waveManager;
    [Inject] private GameManager gameManager;
    [SerializeField] private float intervalSpeed = 7f; 
    private WaveDataConfig _currentWave;
    private float _spawnTimer;
    private float _currentInterval;
    private float _currentIntervalChangeTimer;
    private bool _spawningEnabled;

    private void Awake()
    {
        gameManager.OnRestartWaveEvent += KillAllEnemies;
    }

    private void OnDestroy()
    {
        gameManager.OnRestartWaveEvent -= KillAllEnemies;

    }

    public int CurrentEnemies { get; private set; }

    private void KillAllEnemies()
    {
        foreach (var enemyFactoryEnemy in enemyFactory.Enemies)
        {
            enemyFactoryEnemy.GetDamage(100000);
        }
    }

    public void StartSpawning(WaveDataConfig wave)
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
        _currentIntervalChangeTimer += deltaTime;
        _spawnTimer += deltaTime;
     //   if(_currentIntervalChangeTimer >= 1f)
        {
            float evaluatedCurve = _currentWave.intervalCurve.Evaluate(1f -
                ((float)waveManager.CurrentTime) / ((float)_currentWave.waveDuration));

          //  _currentInterval = (float)(_currentWave.SpawnInterval / evaluatedCurve) / intervalSpeed;
            _currentInterval = (float)( intervalSpeed  / evaluatedCurve) / _currentWave.SpawnInterval ;

            _currentIntervalChangeTimer = 0;
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
    
    public BaseEnemy GetRandomEnemy()
    {
        // Підраховуємо загальну суму шансів
        int totalWeight = _currentWave.Enemies.Sum(e => e.ChanceToSpawn);

        // Генеруємо випадкове число від 0 до totalWeight
        int randomWeight = Random.Range(0, totalWeight);

        // Ітеруємо список і шукаємо, куди потрапляє випадкове число
        foreach (var enemy in _currentWave.Enemies)
        {
            if (randomWeight < enemy.ChanceToSpawn)
                return enemy.SelectedEnemy;

            randomWeight -= enemy.ChanceToSpawn;
        }

        // Якщо щось пішло не так, повертаємо null (хоча це не повинно статись)
        return null;
    }

    private void SpawnRandomGroup()
    {

        var group = GetRandomEnemy();
   
            if (group is PoolAble poolAble)
            {
                var enemy = enemyFactory.Create(poolAble.Type);
                Vector3 pos = randomPointInBoxCollider.GetRandomPointInBox();
                /*NavMeshHit hit;
                if (NavMesh.SamplePosition(pos, out hit, 5f, NavMesh.AllAreas))
                {
                    pos = hit.position;
                }
                enemy.GetComponent<NavMeshAgent>().Warp(pos);*/
                enemy.transform.position = pos;
                enemy.EnemyStateMachine.ChangeState<IdleState>();
                enemy.transform.rotation = Quaternion.identity;
                CurrentEnemies++;
                if( waveManager.CurrentTime <= _timeWhenStopSpawning)
                {
                    StopSpawning();
                }
            }
    }

    public void OnEnemyKilled()
    {
        CurrentEnemies = Mathf.Max(0, CurrentEnemies - 1);
    }
}