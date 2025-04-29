using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameMangerInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [FormerlySerializedAs("weaponManagerView")] [SerializeField] private WeaponCardManagerView weaponCardManagerView;
    
    public override void InstallBindings()
    {
        Container.BindInstance(gameManager).AsSingle();
        Container.BindInstance(weaponCardManagerView).AsSingle();
    }
}

