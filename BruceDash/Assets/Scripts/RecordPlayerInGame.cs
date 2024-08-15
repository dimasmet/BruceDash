using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPlayerInGame
{
    private Text _recordTextView;
    private float _timeRecord;

    public RecordPlayerInGame(Text textField)
    {
        _recordTextView = textField;

        _timeRecord = PlayerPrefs.GetFloat("TimeRecord");
        UpdateTextFieldRecord();
    }

    public bool CheckNewResult(float time)
    {
        if (time > _timeRecord)
        {
            _timeRecord = time;
            PlayerPrefs.SetFloat("TimeRecord", _timeRecord);
            UpdateTextFieldRecord();

            return true;
        }
        else
            return false;
    }

    private void UpdateTextFieldRecord()
    {
        float minutes = Mathf.FloorToInt(_timeRecord / 60);
        float seconds = Mathf.FloorToInt(_timeRecord % 60);
        _recordTextView.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
