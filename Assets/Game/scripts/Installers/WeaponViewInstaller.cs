using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class WeaponViewInstaller : MonoInstaller
{
    [FormerlySerializedAs("weaponManagerView")] [SerializeField] private WeaponCardManagerView weaponCardManagerView;
    public override void InstallBindings()
    {
        Container.BindInstance(weaponCardManagerView).AsSingle();
    }
}