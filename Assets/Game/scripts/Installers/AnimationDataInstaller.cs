using UnityEngine;
using Zenject;

public class AnimationDataInstaller : MonoInstaller
{
    [SerializeField] private CollectableAnimationData _collectableAnimationData;

    public override void InstallBindings()
    {
        Container.BindInstance(_collectableAnimationData).AsSingle();

    }
}