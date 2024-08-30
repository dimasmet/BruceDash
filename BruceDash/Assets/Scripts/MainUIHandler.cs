using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler Instance;

    [SerializeField] private Button _playBtn;

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _recordPanel;
    [SerializeField] private GameObject _storePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _soundSettingsPanel;

    private GameObject _currentActivePanel;

    [SerializeField] private Button _recordOpenButton;
    [SerializeField] private Button _recordCloseButton;

    [SerializeField] private Button _openStoreButton;
    [SerializeField] private Button _closeStoreButton;

    [SerializeField] private RulesViewGame _rulesViewGame;

    public enum NamePanel
    {
        Menu,
        Game,
        Store,
        RecordPlayer,
        Settings,
        SoundSettings
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _playBtn.onClick.AddListener(() =>
        {
            SoundSettings.I.RunSound(SoundSettings.NameSound.Click);
            ActivePanel(NamePanel.Game);

            _rulesViewGame.ShowRulesGame();
        });

        _recordOpenButton.onClick.AddListener(() =>
        {
            SoundSettings.I.RunSound(SoundSettings.NameSound.Click);
            ActivePanel(NamePanel.RecordPlayer);
        });

        _recordCloseButton.onClick.AddListener(() =>
        {
            SoundSettings.I.RunSound(SoundSettings.NameSound.Click);
            ActivePanel(NamePanel.Menu);
        });

        _openStoreButton.onClick.AddListener(() =>
        {
            SoundSettings.I.RunSound(SoundSettings.NameSound.Click);
            ActivePanel(NamePanel.Store);
        });

        _closeStoreButton.onClick.AddListener(() =>
        {
            SoundSettings.I.RunSound(SoundSettings.NameSound.Click);
            ActivePanel(NamePanel.Menu);
        });
    }

    private void Start()
    {
        ActivePanel(NamePanel.Menu);
    }

    public void ActivePanel(NamePanel namePanel)
    {
        HideCurrentPanel();

        switch (namePanel)
        {
            case NamePanel.Menu:
                Time.timeScale = 1;
                _currentActivePanel = _menuPanel;
                break;
            case NamePanel.Game:
                _currentActivePanel = _gamePanel;
                break;
            case NamePanel.RecordPlayer:
                _currentActivePanel = _recordPanel;
                break;
            case NamePanel.Store:
                _currentActivePanel = _storePanel;
                break;
            case NamePanel.Settings:
                _currentActivePanel = _settingsPanel;
                break;
            case NamePanel.SoundSettings:
                _currentActivePanel = _soundSettingsPanel;
                break;
        }

        _currentActivePanel.SetActive(true);
    }

    private void HideCurrentPanel()
    {
        if (_currentActivePanel != null)
        {
            _currentActivePanel.SetActive(false);
        }
    }
}
