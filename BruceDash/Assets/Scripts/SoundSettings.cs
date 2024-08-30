using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Button _open;
    [SerializeField] private Button _close;

    [SerializeField] private Slider slider;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _soundActive;
    [SerializeField] private Sprite _soundNoActive;

    [SerializeField] private Button _vibroBtn;

    [Header("Sounds")]
    public static bool isVibro = true;

    [SerializeField] private AudioSource _backgroundSound;
    [SerializeField] private AudioSource _soundsGame;

    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _clickButton;
    [SerializeField] private AudioClip _desctroySound;

    public static SoundSettings I;

    public enum NameSound
    {
        Coin,
        Lose,
        Click,
        Destroy
    }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

        _open.onClick.AddListener(() =>
        {
            MainUIHandler.Instance.ActivePanel(MainUIHandler.NamePanel.SoundSettings);
        });

        _close.onClick.AddListener(() =>
        {
            MainUIHandler.Instance.ActivePanel(MainUIHandler.NamePanel.Menu);
        });

        _vibroBtn.onClick.AddListener(() =>
        {
            isVibro = !_vibroBtn;

            if (isVibro)
            {
                _vibroBtn.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                _vibroBtn.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        });
    }

    public void ChangeSliderValue()
    {
        if (slider.value < 0.1f)
        {
            _soundImage.sprite = _soundNoActive;
        }
        else
        {
            _soundImage.sprite = _soundActive;
        }

        _backgroundSound.volume = slider.value;
        _soundsGame.volume = slider.value;
    }

    public void RunSound(NameSound name)
    {
        switch (name)
        {
            case NameSound.Coin:
                _soundsGame.PlayOneShot(_coinSound);
                break;
            case NameSound.Lose:
                _soundsGame.PlayOneShot(_loseSound);
                break;
            case NameSound.Click:
                _soundsGame.PlayOneShot(_clickButton);
                break;
            case NameSound.Destroy:
                _soundsGame.PlayOneShot(_desctroySound);
                break;
        }
    }
}
