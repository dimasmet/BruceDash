using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.iOS;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button _open;
    [SerializeField] private Button _close;

    [SerializeField] private Button _rateUsBtn;
    [SerializeField] private Button _privacyBtn;
    [SerializeField] private Button _termsBtn;

    [SerializeField] private GameObject _textPanel;
    [SerializeField] private Text _titleText;
    [SerializeField] private Button _closeTextPanel;
    [SerializeField] private GameObject _privacyText;
    [SerializeField] private GameObject _termsText;

    private void Awake()
    {
        _open.onClick.AddListener(() =>
        {
            MainUIHandler.Instance.ActivePanel(MainUIHandler.NamePanel.Settings);
        });

        _close.onClick.AddListener(() =>
        {
            MainUIHandler.Instance.ActivePanel(MainUIHandler.NamePanel.Menu);
        });

        _rateUsBtn.onClick.AddListener(() =>
        {
            Device.RequestStoreReview();
        });

        _closeTextPanel.onClick.AddListener(() =>
        {
            _textPanel.SetActive(false);
        });

        _privacyBtn.onClick.AddListener(() =>
        {
            _titleText.text = "PRIVACY POLICY";
            _textPanel.SetActive(true);
            _privacyText.SetActive(true);
            _termsText.SetActive(false);
        });

        _termsBtn.onClick.AddListener(() =>
        {
            _titleText.text = "TERMS OF USE";
            _textPanel.SetActive(true);
            _privacyText.SetActive(false);
            _termsText.SetActive(true);
        });
    }
}
