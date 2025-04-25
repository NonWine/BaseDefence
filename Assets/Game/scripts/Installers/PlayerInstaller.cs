using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private PlayerCombatManager playerCombatManager;
    [Inject] private GameController _gameController;
    private PlayerContainer _playerContainer;
        
    public override void InstallBindings()
    {
        BindHandlers();
        RegirsterPlayer();
    }

    private void BindHandlers()
    {
        Container.BindInstance(playerCombatManager).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMonitor>().FromNew().AsSingle();
        Container.Bind<OverlapSphereHandler>().FromNew().AsSingle();
        Container.Bind<StorageManager>().FromNew().AsSingle();
        Container.BindInstance(_joystick).AsSingle().NonLazy();
        Container.Bind<PlayerGiveDamageHandler>().FromNew().AsSingle();
    }

    private void RegirsterPlayer()
    {
       var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab);
       player.transform.position = playerSpawnPoint.position;
           _playerContainer = _playerPrefab.PlayerContainer;
           player.Initialize();
        Container.BindInstance(_playerContainer).AsSingle().Lazy();
        Container.BindInstance(player).AsSingle();
       _gameController.Player = player;
    }
}