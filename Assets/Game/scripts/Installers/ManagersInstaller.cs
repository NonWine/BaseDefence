using UnityEngine;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SpawnManager spawnManager;

        
    public override void InstallBindings()
    {
        Container.BindInstance(gameManager).AsSingle();
        Container.BindInstance(levelManager).AsSingle();
        Container.BindInstance(spawnManager).AsSingle();
    }



}