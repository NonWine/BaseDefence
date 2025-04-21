using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour
{
   [SerializeField] private  WaveData _waveData;
   [Inject] private  EnemySpawner _spawner;
   [Inject] private  IPlayerMonitor _playerMonitor;

    private float _waveTimer;
    private bool _waveActive;

    [Button]
    public void StartWave()
    {
        _waveTimer = 0f;
        _waveActive = true;
        _spawner.StartSpawning(_waveData);
    }

    public void Update()
    {
        if (!_waveActive) return;

        _waveTimer += Time.deltaTime;
        _spawner.UpdateSpawner(Time.deltaTime);

        if (_playerMonitor.AverageKillTime < 1.5f)
            _spawner.TemporarilyIncreaseSpawnRate();

        if (_playerMonitor.AliveEnemies < 3)
            _spawner.SpawnExtraGroup();

        if (_waveTimer >= _waveData.Duration)
            EndWave();
    }

    private void EndWave()
    {
        _waveActive = false;
        _spawner.StopSpawning();
        // Trigger end of wave events here
    }
}