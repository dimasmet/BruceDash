using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlocks : MovingObject
{
    [SerializeField] private ObjectGame _objectGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EndMapTrigger end))
        {
            if (_objectGame != null)
                _objectGame.ReturnToPool();
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
