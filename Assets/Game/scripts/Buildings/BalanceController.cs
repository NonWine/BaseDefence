using System;
using UnityEngine;
using Zenject;

public class BalanceController : MonoBehaviour , ITickable
{
    [Range(1, 100)] public int DamageBonusing;
    [SerializeField] private float _restTime;
    [Inject] private GameController _gameController;
    [SerializeField] private UnitSpawnerBuilding _spawnerBuilding;
    public bool BonuseAvaiable { get; private set; }
    
    private float _timer;

    private void Start()
    {
        _gameController.RegisterInTick(this);
        _spawnerBuilding.OnSpawnUnit += ChangeBalance; 

    }

    private void OnDestroy()
    {
        _spawnerBuilding.OnSpawnUnit -= ChangeBalance; 

    }


    public void Tick()
    {
        if(BonuseAvaiable)
            return;
        
        _timer += Time.deltaTime;
        if (_timer >= _restTime)
        {        

            BonuseAvaiable = true;
            Debug.Log($"Increase Power!" );
        }
        
    }

    private void ChangeBalance(BaseEnemy targetable)
    {
        if(BonuseAvaiable)
            targetable.CurrentDamage += DamageBonusing;
    }
}