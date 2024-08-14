using UnityEngine;

public class CheckerSuccessCircleLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ChipPlayer chip))
        {
            GameMain.OnCircleLevelOvercome?.Invoke();
        }
    }
}
