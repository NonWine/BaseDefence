using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelData", fileName = "WaveData", order = 0)]

public class LevelData : ScriptableObject
{
   [SerializeField] public List<WaveDataConfig> wavesData;

   [SerializeField] public List<RewardContainer> RewardsContainer = new List<RewardContainer>();
}

[System.Serializable]
public struct RewardContainer
{
    public WalletObj Type;
    public int Count;
}