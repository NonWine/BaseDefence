using UnityEngine;
using Zenject;

public class CollectableManagerInstaller : MonoInstaller
{
    [SerializeField] private CollectableManager _collectableManager;
    [SerializeField] private CollectableAnimationData CollectableAnimationData;
    private StorageManager StorageManager;
    
    public override void InstallBindings()
    {
        StorageManager = new StorageManager();
        Container.BindInstance(CollectableAnimationData);
        Container.Bind<StorageManager>().FromInstance(StorageManager).AsSingle();
        BindResourcesFactory();
    }

    private void BindResourcesFactory()
    {
        Container.BindInstance(_collectableManager);
    }
}