using System.Collections;
using System.Collections.Generic;
using RichTap.Source;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSkinChip
{
    public Sprite skin;
    public Color colorParticles;
}

public class ChipPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRender;
    [SerializeField] private SpriteRenderer[] _spriteObjects;
    [SerializeField] private DataSkinChip[] _skinsData;

    [SerializeField] private Rigidbody2D _rbBall;
    [SerializeField] private float _forceJump;

    [SerializeField] private ParticleSystem _particleSystem;

    private bool isGround = false;

    [SerializeField] private GameObject _prefabsDestoroyObjects;

    private GameObject destoryObj;

    private Vector2 posStart;

    private bool isControl = false;

    [SerializeField] private RichtapClipEffect effect;

    private void Start()
    {
        posStart = transform.position;

        GameMain.OnClearScene += ResetChipPlayer;
        GameMain.OnClearScene += StartControl;
        GameMain.OnEndGame += StopControl;
    }

    private void OnDestroy()
    {
        GameMain.OnClearScene -= ResetChipPlayer;
        GameMain.OnClearScene -= StartControl;
        GameMain.OnEndGame -= StopControl;
    }

    private void StartControl()
    {
        isControl = true;
    }

    private void StopControl()
    {
        isControl = false;
    }

    public void SetSkin(int numberSkin)
    {
        DataSkinChip skin = _skinsData[numberSkin];
        _particleSystem.startColor = skin.colorParticles;
        _spriteRender.sprite = skin.skin;

        for (int i = 0; i < _spriteObjects.Length; i++)
        {
            _spriteObjects[i].color = skin.colorParticles;
        }

    }

    private void ResetChipPlayer()
    {
        if (destoryObj != null)
        {
            Destroy(destoryObj);
        }
        transform.position = posStart;
        GetComponent<CircleCollider2D>().enabled = true;
        _rbBall.isKinematic = false;
        transform.GetChild(0).gameObject.SetActive(true);
        _particleSystem.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ground ground))
        {
            isGround = true;
        }

        if (collision.gameObject.TryGetComponent(out WallObject wall))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            destoryObj = Instantiate(_prefabsDestoroyObjects, _prefabsDestoroyObjects.transform.parent);
            destoryObj.SetActive(true);

            _rbBall.isKinematic = true;
            GetComponent<CircleCollider2D>().enabled = false;
            _particleSystem.Stop();
            GameMain.OnEndGame?.Invoke();
            GameMain.OnResultGame?.Invoke();

            SoundSettings.I.RunSound(SoundSettings.NameSound.Destroy);
            if (SoundSettings.isVibro)
                effect.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out StarObject star))
        {
            star.CollectCoin();
            BalancePlayer.OnAddedBalance?.Invoke(1);

            SoundSettings.I.RunSound(SoundSettings.NameSound.Coin);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ground ground))
        {
            isGround = false;
        }
    }

    private void Update()
    {
        if (isControl)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Stationary && isGround)
                {
                    _rbBall.AddForce(Vector2.up * _forceJump);
                    isGround = false;
                }
            }
        }
    }
}
