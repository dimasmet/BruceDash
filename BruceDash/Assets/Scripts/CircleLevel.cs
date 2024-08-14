using UnityEngine;

public class CircleLevel : MovingObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ChipPlayer chipPlayer))
        {
            if (gameObject.layer == 4)
                GameMain.OnCircleLevelSuccess?.Invoke();
        }
    }
}
