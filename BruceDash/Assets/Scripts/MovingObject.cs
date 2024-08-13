using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] protected float _speedMove;

    [SerializeField] private ObjectGame _objectGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EndMapTrigger end))
        {
            if (_objectGame != null)
            _objectGame.ReturnToPool();
        }
    }

    protected void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * _speedMove * Time.deltaTime;
    }
}
