using UnityEngine;
using Zenject;

public class GameMangerInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private WeaponManagerView weaponManagerView;
    
    public override void InstallBindings()
    {
        Container.BindInstance(gameManager).AsSingle();
        Container.BindInstance(weaponManagerView).AsSingle();
    }
}

