using UnityEngine;
using Zenject;

public class WindowsInstaller : MonoInstaller
{
    [SerializeField] private WindowsController _windowsController;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_windowsController).AsSingle();
    }
}