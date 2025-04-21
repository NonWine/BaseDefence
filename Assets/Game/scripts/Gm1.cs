using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Gm1 : MonoBehaviour
{
    [Inject] public GameController _gameController;
    void Start()
    {
        Debug.Log(_gameController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
