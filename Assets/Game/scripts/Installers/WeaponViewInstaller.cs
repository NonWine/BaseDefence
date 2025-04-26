using UnityEngine;
using Zenject;

public class WeaponViewInstaller : MonoInstaller
{
    [SerializeField] private WeaponManagerView weaponManagerView;
    public override void InstallBindings()
    {
        Container.BindInstance(weaponManagerView).AsSingle();
    }
}