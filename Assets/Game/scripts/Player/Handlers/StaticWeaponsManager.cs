using System.Collections.Generic;
using UnityEngine;

public class StaticWeaponsManager : MonoBehaviour
{
    [SerializeField] private List<StaticWeaponController> weaponControllers;

    public List<StaticWeaponController> OpenedControllers => weaponControllers.FindAll(x => x.IsLocked == false);

}