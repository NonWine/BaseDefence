using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PlayerHandler playerHandler;
    [SerializeField] private PlayerLevelController playerLevelController;
    [SerializeField] private StaticWeaponsManager staticWeaponsManager;
    private PlayerContainer _playerContainer;
        
    public override void InstallBindings()
    {
        BindHandlers();
    }

    private void BindHandlers()
    {
        Container.BindInstance(playerLevelController).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMonitor>().FromNew().AsSingle();
        Container.Bind<OverlapSphereHandler>().FromNew().AsSingle();
        Container.BindInstance(_joystick).AsSingle().NonLazy();
        Container.BindInstance(playerHandler).AsSingle();
        Container.BindInstance(staticWeaponsManager).AsSingle();
    }
    
}