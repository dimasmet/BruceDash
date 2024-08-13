using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbBall;
    [SerializeField] private float _forceJump;

    private bool isGround = false;

    private void Awake()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ground ground))
        {
            isGround = true;
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
