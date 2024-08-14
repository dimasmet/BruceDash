using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] protected float _speedMove;

    protected void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * _speedMove * Time.deltaTime;
    }
}
