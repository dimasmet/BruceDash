using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private SpawnerGame _spawnerGame;

    [SerializeField] private Button _openPauseButton;

    [SerializeField] private Button _continuemButton;
    [SerializeField] private Button _menuButton;

    [SerializeField] private GameObject _panelPause;

    private void Awake()
    {
        _openPauseButton.onClick.AddListener(() =>
        {
            _panelPause.SetActive(true);
            Time.timeScale = 0;
        });

        _continuemButton.onClick.AddListener(() =>
        {
            _panelPause.SetActive(false);
            _spawnerGame.BackToLevelSpeedTime();
        });

        _menuButton.onClick.AddListener(() =>
        {
            _panelPause.SetActive(false);
            MainUIHandler.Instance.ActivePanel(MainUIHandler.NamePanel.Menu);
            GameMain.OnEndGame?.Invoke();
        });
    }
}
