using UnityEngine;

public class StarObject : MonoBehaviour
{
    public void CollectCoin()
    {
        gameObject.SetActive(false);
    }

    public void ActiveStar()
    {
        gameObject.SetActive(true);
    }
}
