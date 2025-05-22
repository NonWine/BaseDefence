using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveStartWeapon : MonoBehaviour
{
    [SerializeField] WeaponCardManagerView weaponCardManagerView;
    [SerializeField] WeaponInfoData[] pushki;
    [SerializeField] PlayerCombatManager playerCombatManger;
    
    public void CreateStartWeapon()
    {
        Debug.Log("start");
        weaponCardManagerView.OnGetWeaponEvent += GivePushki;
        weaponCardManagerView.CreateCards(pushki,true);
    }
    

    private void GivePushki(WeaponInfoData weaponInfoData)
    {
        playerCombatManger.defaultWeapon = (DynamicWeapon)weaponInfoData;
        playerCombatManger.Init();
        weaponCardManagerView.OnGetWeaponEvent -= GivePushki;

    }


}
