using UnityEngine;
using Zenject;

public class ScreenTouchControllerInstaller : MonoInstaller
{
    [SerializeField] private ScreenTouchController _screenTouchController;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_screenTouchController).AsSingle().NonLazy();
    }
    
}