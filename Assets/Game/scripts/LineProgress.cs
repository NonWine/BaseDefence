using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LineProgress : MonoBehaviour
{
    [SerializeField] private Image _filledLine;
    [SerializeField] private float _speedLerp;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private float incomeTime;
    private int countMobs;
    private float percent;
    private float forpercent;
    private float lineFilled;
    private float time = 0;

    public void SetMobs(int value)
    {
        _filledLine.fillAmount = 0f;
        countMobs = value;
        forpercent = countMobs;
    } 

    private IEnumerator FillLine(float count)
    {
        float speed = 0;
        percent = ((forpercent - (countMobs - count)) / forpercent);
        lineFilled += percent;
        while (_filledLine.fillAmount != lineFilled)
        {
            speed += Time.deltaTime;
            _filledLine.fillAmount = Mathf.Lerp(_filledLine.fillAmount, lineFilled, speed * _speedLerp);
            if(_filledLine.fillAmount >= 1f)
            {
                yield break;
              //  LevelUIManager.Instance.gameObject.SetActive(true);
            }
            yield return null;
        }
    }

    private IEnumerator IncomeCor()
    {
        waveText.text = "Wave " + LevelManager.Instance.VisualCurrentLevel + " Incoming";
        while (time <= incomeTime)
        {
            _filledLine.fillAmount = Mathf.Lerp(1f, 0f, ((time / incomeTime)));
            time  += Time.deltaTime;
            yield return null;
        }
        time = 0f;
        waveText.text = "WAVE";
        SpawnManager.Instance.Spawn();
        yield break;
    }

    public void ChangeLevelWave()
    {
        lineFilled = 0f;
        _filledLine.fillAmount = 0f;
     //    _from.text = LevelUIManager.Instance.VisualCurrentLevelUI.ToString();
    


    }

    public void UpdateLineProgress() => StartCoroutine(FillLine(1));

    public void Incoming()
    {
        RestartLineProgress();
        StartCoroutine(IncomeCor());
    }

    public void RestartLineProgress()
    {
        StopAllCoroutines();
        lineFilled = 0f;
        _filledLine.fillAmount = 0f;
    }

    public float GetFillAmount() { return _filledLine.fillAmount; }
}
