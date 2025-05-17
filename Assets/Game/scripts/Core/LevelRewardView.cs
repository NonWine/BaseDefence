using System.Collections.Generic;
using UnityEngine;

public class LevelRewardView : MonoBehaviour
{
    [SerializeField] private RewardIconView rewardIconsView;
    [SerializeField] private RectTransform container;
    private List<RewardIconView> currentRewards = new List<RewardIconView>();
    
    public void CreateRewards(LevelData levelData)
    {
        foreach (var rewardData in levelData.RewardsContainer)
        {
            var reward = Instantiate(rewardIconsView, container);
            reward.Initialize(rewardData.Type.ItemIcon, rewardData.Count);
            currentRewards.Add(reward);
        }
    }

    public void DestroyRewards()
    {
        for (var i = currentRewards.Count - 1; i >= 0; i--)
        { 
            Destroy(currentRewards[i].gameObject);
        }
        currentRewards.Clear();
    }
}