using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.AI;
using Zenject;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private LineProgress lineProgress;
    [SerializeField] private SpawnConfiguration _spawnConfiguration;
    [Inject] private  EnemyFactory enemyFactory;
    [Inject] private LevelManager _levelManager;
    [Inject] private Player player;
    [Inject] private GameManager gameManager;
    private int waveId;
    private int generalCounts;
    public event Action OnSpawn;

    private void Awake()
    {
        _levelManager.OnFinishLevel += AddLevelWave;
        Instance = this;
        _spawnConfiguration.TimeDelay = PlayerPrefs.GetFloat("timeDelay", _spawnConfiguration.TimeDelay);
        waveId = PlayerPrefs.GetInt("waveId", 0); // start LEVEL\Get Current LEVEL
    }

    private void OnDestroy()
    {
        _levelManager.OnFinishLevel -= AddLevelWave;
    }

    public void Spawn()
    {
        StartCoroutine(SpawnCor());
        OnSpawn?.Invoke();
    }

    private IEnumerator SpawnCor()
    {
        int j = 0;
        Transform point;
        for (int i = 0; i <= waves[waveId].Count[j]; i++)
        {
            if (i == waves[waveId].Count[j])
            {
                i = 0;
                j++;
                if (j == waves[waveId].Count.Length)
                    break;
            }
            point = spawnPoints.ToList().Find(x => Vector3.Distance(x.position, player.transform.position) > 47f);
            spawnPoints = spawnPoints.ToList().OrderBy(x => Guid.NewGuid()).ToArray();
            BaseEnemy unit = waves[waveId].Mob[j].GetComponent<BaseEnemy>();
            var enemy = enemyFactory.Create(unit.GetType());
            Vector3 pos = point.position;
            pos.y = 4f;
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pos, out hit, 5f, NavMesh.AllAreas))
            {
                pos = hit.position;
                
            }

            enemy.GetComponent<NavMeshAgent>().Warp(pos + Vector3.up * enemy.GetComponent<NavMeshAgent>().baseOffset);
            enemy.EnemyStateMachine.ChangeState<IdleState>();
            enemy.transform.rotation = Quaternion.identity;
            generalCounts++;
        }
        OnSpawn?.Invoke();
        lineProgress.SetMobs(generalCounts);
        yield break;
    }
    
    public void AddDeathEnemy()
    {
        generalCounts--;
        if (generalCounts == 0)
        {
          gameManager.GameWin();
        }
        else 
            lineProgress.UpdateLineProgress();

    }

    private void AddLevelWave()
    {
        waveId++;
        PlayerPrefs.SetInt("waveId", waveId);
        _spawnConfiguration.TimeDelay -= _spawnConfiguration.ReduceDelay;
        if ( _spawnConfiguration.TimeDelay < _spawnConfiguration.TimeLimitDelay)  _spawnConfiguration.TimeDelay = _spawnConfiguration.TimeLimitDelay;
        PlayerPrefs.SetFloat("timeDelay",  _spawnConfiguration.TimeDelay);
    }

    public void OffTutors()
    {
    }
}