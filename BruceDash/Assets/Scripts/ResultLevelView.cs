using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLevelView : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel;

    [SerializeField] private Text _resultTimeText;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Animator _animation;

    private void Awake()
    {
        _replayButton.onClick.AddListener(() =>
        {
            _resultPanel.SetActive(false);
            MainUIHandler.Instance.ActivePanel(MainUIHandler.NamePanel.Game);
            GameMain.Instance.FirstStartLevel();

            _animation.Play("idle");
        });

        _closeButton.onClick.AddListener(() =>
        {
            _resultPanel.SetActive(false);
            MainUIHandler.Instance.ActivePanel(MainUIHandler.NamePanel.Menu);

            _animation.Play("idle");
        });
    }

    public void ShowResultGame(string timeResult, bool isRecord)
    {
        _resultPanel.SetActive(true);
        _resultTimeText.text = timeResult;

        _animation.Play("Open");

        SoundSettings.I.RunSound(SoundSettings.NameSound.Lose);
    }
}
