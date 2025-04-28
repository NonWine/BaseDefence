using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPHandlerView : MonoBehaviour
{

  [SerializeField] private TMP_Text currentLevelText;
  [SerializeField] private float animationDurationPerStep = 0.5f;
  [SerializeField] private Slider slider;
  
  private Sequence levelUpSequence; // DOTween Sequence для послідовних анімацій

  public event Action OnUpdateEvent; 

  public void SetCurrentLevelView(int level)
  {
      currentLevelText.text = level.ToString();
  }

  public void SetLevelData(float value, float maxValue)
  {
      slider.value = value;
      slider.maxValue = maxValue;
  }

  public void AnimateSlider(int currentExperience, int currentExperienceRequired, System.Action LevelUpCallback)
  {
      TryToKillPrevAnimation();

      levelUpSequence = DOTween.Sequence();

      if (currentExperience < currentExperienceRequired)
      {
          // Просто анімація досвіду
          levelUpSequence.Append(
              slider.DOValue(currentExperience, animationDurationPerStep).SetEase(Ease.OutQuad)
          );
      }
      else
      {
          // Заповнюємо до кінця
          levelUpSequence.Append(
              slider.DOValue(currentExperienceRequired, animationDurationPerStep).SetEase(Ease.OutQuad)
          );

          levelUpSequence.AppendCallback(() =>
          {
              // Тряска
              transform
                  .DOShakeScale(0.5f, strength: Vector3.one * 0.2f, vibrato: 10, randomness: 90)
                  .SetEase(Ease.OutBack);

              currentExperience -= currentExperienceRequired;
              if (currentExperience <= 0)
                  currentExperience = 0;

              LevelUpCallback?.Invoke();  // Підняли рівень → оновилось currentExperienceRequired
              OnUpdateEvent?.Invoke();    // Оновили UI після LevelUp

              AnimateSlider(currentExperience, currentExperienceRequired, LevelUpCallback);
          });
      }
  }

  
  public void TryToKillPrevAnimation()
  {
      if (levelUpSequence != null && levelUpSequence.IsActive())
      {
          levelUpSequence.Kill();
      }
      levelUpSequence = DOTween.Sequence();
  }
}


