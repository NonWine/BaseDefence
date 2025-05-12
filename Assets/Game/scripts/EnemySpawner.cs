using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;
    [SerializeField] private int _timeWhenStopSpawning;
    [Inject] private EnemyFactory enemyFactory;
    [Inject] WaveManager waveManager;
    [SerializeField] private float intervalSpeed = 7f; 
    private WaveDataConfig _currentWave;
    private float _spawnTimer;
    private float _currentInterval;
    private float _currentIntervalChangeTimer;
    private bool _spawningEnabled;



    public int CurrentEnemies { get; private set; }

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

    private void SpawnRandomGroup()
    {
        var group = _currentWave.Enemies[Random.Range(0, _currentWave.Enemies.Count)];
        for (int i = 0; i < group.Count; i++)
        {
            if (group.SelectedEnemy is PoolAble poolAble)
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