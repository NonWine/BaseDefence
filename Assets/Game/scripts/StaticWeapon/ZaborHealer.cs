using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZaborHealer : StaticWeaponObj
{
    [Inject] private Target target;
    private float _healTimer;
}