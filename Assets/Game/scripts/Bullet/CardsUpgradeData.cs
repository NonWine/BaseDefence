using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "CardsUpgradeData", fileName = "CardsUpgradeData", order = 0)]
public class CardsUpgradeData : SerializedScriptableObject
{
  [ListDrawerSettings(DraggableItems = false, NumberOfItemsPerPage = 3)]  public List<CardUpgradeInfo> Upgrades = new List<CardUpgradeInfo>();
}