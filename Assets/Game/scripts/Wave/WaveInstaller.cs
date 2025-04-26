using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaveInstaller : MonoInstaller
{
    [SerializeField] private WaveManager waveManager;
    public override void InstallBindings()
    {
        Container.BindInstance(waveManager).AsSingle();
    }
}
