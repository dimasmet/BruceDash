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

    private GameObject _currentActivePanel;

    [SerializeField] private Button _recordOpenButton;
    [SerializeField] private Button _recordCloseButton;

    [SerializeField] private Button _openStoreButton;
    [SerializeField] private Button _closeStoreButton;

    public enum NamePanel
    {
        Menu,
        Game,
        Store,
        RecordPlayer
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _playBtn.onClick.AddListener(() =>
        {
            ActivePanel(NamePanel.Game);

            GameMain.Instance.FirstStartLevel();
        });

        _recordOpenButton.onClick.AddListener(() =>
        {
            ActivePanel(NamePanel.RecordPlayer);
        });

        _recordCloseButton.onClick.AddListener(() =>
        {
            ActivePanel(NamePanel.Menu);
        });

        _openStoreButton.onClick.AddListener(() =>
        {
            ActivePanel(NamePanel.Store);
        });

        _closeStoreButton.onClick.AddListener(() =>
        {
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
