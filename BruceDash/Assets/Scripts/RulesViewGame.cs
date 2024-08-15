using UnityEngine;
using UnityEngine.UI;

public class RulesViewGame : MonoBehaviour
{
    [SerializeField] private GameObject _rules;
    [SerializeField] private GameObject[] _rulesPanel;

    [SerializeField] private Button _okButton;

    private int numberPanel;

    private GameObject _currentPanel;

    private void Awake()
    {
        _okButton.onClick.AddListener(() =>
        {
            numberPanel++;

            if (numberPanel < _rulesPanel.Length)
                Show();
            else
            {
                GameMain.Instance.FirstStartLevel();
                _rules.SetActive(false);
            }
        });
    }

    public void ShowRulesGame()
    {
        _rules.SetActive(true);
        numberPanel = 0;
        Show();
    }

    private void Show()
    {
        if (_currentPanel != null) _currentPanel.SetActive(false);

        _currentPanel = _rulesPanel[numberPanel];
        _currentPanel.SetActive(true);
    }
}
