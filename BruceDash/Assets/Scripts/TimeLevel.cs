using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLevel : MonoBehaviour
{
    private float _time;
    [SerializeField] private Text timerText;

    private IEnumerator StartTimer()
    {
        while (true)
        {
            _time += Time.deltaTime / GameMain.currentLevel.speedMove;
            UpdateTimeText();
            yield return null;
        }
    }

    public void StartTime()
    {
        _time = 0;
        StartCoroutine(StartTimer());
    }

    public float GetResultTime()
    {
        StopAllCoroutines();
        return _time;
    }

    public string GetTimeResultString()
    {
        return string.Format("{0:00}:{1:00}", Mathf.FloorToInt(_time / 60), Mathf.FloorToInt(_time % 60));
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(_time / 60);
        float seconds = Mathf.FloorToInt(_time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
