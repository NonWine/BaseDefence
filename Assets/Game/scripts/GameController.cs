using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    
    private List<ITickable> _tickables; 
    private DiContainer _diContainer;

    public void RegisterInTick(ITickable tickable)
    {
        if (!_tickables.Contains(tickable))
        {
            _tickables.Add(tickable);
        }
    }
    
    private void Awake()
    {
        _tickables = new List<ITickable>();
        
    }

    private void Update()
    {
        foreach (var tickable in _tickables)
        {
            tickable.Tick();
        }
    }
    
}


