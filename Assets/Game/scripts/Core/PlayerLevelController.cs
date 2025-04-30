using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    
    [Header("Level Settings")]
    [SerializeField] private int startExperienceRequired = 100;  // Скільки потрібно досвіду на 1 рівень
    [SerializeField] private float experienceGrowthMultiplier = 1.2f; // Наскільки зростає потреба кожного рівня
    [SerializeField] private XPHandlerView xpHandlerView;
    [ProgressBar(1, 5)] [SerializeField] private float modificator = 1f;
    [SerializeField,ReadOnly] private int currentLevel = 1;
    [SerializeField,ReadOnly]  private int currentExperience = 0;
    [SerializeField,ReadOnly]  private int currentExperienceRequired;

    public static event Action OnLevelUp;


    private void Start()
    {
        currentExperienceRequired = startExperienceRequired;
        UpdateLevelData();
        xpHandlerView.OnUpdateEvent += UpdateLevelData;
    }

    private void OnDestroy()
    {
        xpHandlerView.OnUpdateEvent -= UpdateLevelData;

    }

    public void ImproveModificator(float addValue) => modificator += addValue;

    [Button]
    public void AddExperience(int amount)
    {
        xpHandlerView.TryToKillPrevAnimation(); // Зупиняємо попередні анімації
        currentExperience += Mathf.FloorToInt(amount * modificator);
        xpHandlerView.AnimateSlider(currentExperience, currentExperienceRequired, HandleLevelUp);
    }

    private void HandleLevelUp()
    {
        currentLevel++;
        currentExperience -= currentExperienceRequired;
        if (currentExperience < 0)
            currentExperience = 0;
        currentExperienceRequired = Mathf.RoundToInt((currentExperienceRequired * experienceGrowthMultiplier));
        xpHandlerView.SetCurrentLevelView(currentLevel);

        Debug.Log($"Level Up! New Level: {currentLevel}");

        OnLevelUp?.Invoke();
    }

    private void UpdateLevelData()
    {
        xpHandlerView.SetLevelData(currentExperience, currentExperienceRequired);
        xpHandlerView.SetCurrentLevelView(currentLevel);
    }
}
