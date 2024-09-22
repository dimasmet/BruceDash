using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DayBonusValue
{
    public int num = 0;
    public int valueCountStars;
}

[System.Serializable]
public class DateLastRunGame
{
    public int day;
    public int month;
    public int year;
}

public class DayBonusStars : MonoBehaviour
{
    [SerializeField] private DayBonusValue[] dayBonusValue;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _dayNumber;
    [SerializeField] private Text _valueCountStars;
    [SerializeField] private Text _dateText;

    [SerializeField] private Button _okBtn;

    [SerializeField] private DateLastRunGame dateLastRunGame;

    public DateTime CurrentDate;
    private DateTime _lastDate;

    private int numberBonus;

    private void Awake()
    {
        _okBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(false);

            BalancePlayer.OnAddedBalance?.Invoke(dayBonusValue[numberBonus].valueCountStars);
        });

        CheckDateCurrentDayBonus();
    }

    public void CheckDateCurrentDayBonus()
    {
        CurrentDate = DateTime.Now;

        _dateText.text = CurrentDate.ToShortDateString();

        string jsDayLast = PlayerPrefs.GetString("LastDayBonus");

            if (jsDayLast != "")
            {
                numberBonus = PlayerPrefs.GetInt("BonusNumber");

                if (numberBonus < 6)
                {
                    numberBonus++;

                    dateLastRunGame = JsonUtility.FromJson<DateLastRunGame>(jsDayLast);

                    _lastDate = new DateTime(dateLastRunGame.year, dateLastRunGame.month, dateLastRunGame.day);

                    if ((int)(CurrentDate - _lastDate).TotalDays == 1)
                    {
                        ShowNewDayBonus();
                    }
                    else
                    {
                        if (((int)(CurrentDate - _lastDate).TotalDays > 1))
                        {
                            Debug.Log("Dat lose");
                            PlayerPrefs.SetString("LastDayBonus", "");
                        }
                        else
                        {
                            Debug.Log("Bonus was taked");
                        }
                    }
                }
                else
                {
                    Debug.Log("new weak");
                    NewWeakBonuses();
                }
            }
            else
            {
                NewWeakBonuses();
            }
        
    }

    private void ShowNewDayBonus()
    {
        Debug.Log("New Day");

        dateLastRunGame = new DateLastRunGame();
        dateLastRunGame.day = CurrentDate.Day;
        dateLastRunGame.month = CurrentDate.Month;
        dateLastRunGame.year = CurrentDate.Year;

        _valueCountStars.text = dayBonusValue[numberBonus].valueCountStars.ToString();
        _dayNumber.text = "DAY " + (numberBonus + 1).ToString();

        PlayerPrefs.SetString("LastDayBonus", JsonUtility.ToJson(dateLastRunGame));
        PlayerPrefs.SetInt("BonusNumber", numberBonus);

        _panel.SetActive(true);
    }

    private void NewWeakBonuses()
    {
        Debug.Log("New Weak");
        numberBonus = 0;
        ShowNewDayBonus();
    }
}
