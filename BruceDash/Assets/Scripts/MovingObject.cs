using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public static bool ISMove = false;

    [SerializeField] protected float _speedMove;

    protected void Start()
    {
        GameMain.OnClearScene += HideObject;
    }

    protected void OnDestroy()
    {
        GameMain.OnClearScene -= HideObject;
    }

    protected void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected void Update()
    {
        if (ISMove)
            transform.position += new Vector3(-1, 0, 0) * _speedMove * Time.deltaTime;
    }
}
