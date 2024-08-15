using UnityEngine;

public class CircleLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ChipPlayer chipPlayer))
        {
             GameMain.OnCircleLevelSuccess?.Invoke();
        }
    }
}
