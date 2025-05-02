using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZaborHealer : MonoBehaviour
{
    [Inject] private Target target;
    [SerializeField] private float _healColdown;
    [SerializeField] private int _healAmount;
    private float _healTimer;

    private void Update()
    {
        _healTimer += Time.deltaTime;
        if(_healTimer >= _healColdown)
        {
            target.Heal(_healAmount);
            _healTimer = 0;
        }
    }
}
