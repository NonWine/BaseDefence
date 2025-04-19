using UnityEngine;
using Zenject;

public class RoadManagerInstaller : MonoInstaller
{
    [SerializeField] private RoadManager _roadManager;
    
    
    public override void InstallBindings()
    {
        Container.BindInstance(_roadManager).AsSingle();
        Container.Bind<UnitRoadTemplate>().AsTransient(); 
        
    }

    
}