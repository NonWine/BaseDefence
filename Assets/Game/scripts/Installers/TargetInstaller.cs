using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TargetInstaller : MonoInstaller
{
    [SerializeField] private Target target;
    public override void InstallBindings()
    {
        Container.BindInstance(target).AsSingle();
    }
}
