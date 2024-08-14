using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbBall;
    [SerializeField] private float _forceJump;

    private bool isGround = false;

    [SerializeField] private GameObject _prefabsDestoroyObjects;

    private GameObject destoryObj;

    private Vector2 posStart;

    private void Awake()
    {

    }

    private void Start()
    {
        posStart = transform.position;

        GameMain.OnStartGame += ResetChipPlayer;
    }

    private void OnDestroy()
    {
        GameMain.OnStartGame -= ResetChipPlayer;
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

            GameMain.OnEndGame?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out StarObject star))
        {
            star.CollectCoin();
            BalancePlayer.OnAddedBalance?.Invoke(1);
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
