using UnityEngine;

public class StarListHandler : MonoBehaviour
{
    [SerializeField] private StarObject[] _stars;

    public void OpenStars()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].ActiveStar();
        }
    }
}
