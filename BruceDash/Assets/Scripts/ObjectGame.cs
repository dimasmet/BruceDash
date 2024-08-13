using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGame : MonoBehaviour
{
    private GameObjectsPool _pool;

    public void SetObjectPool(GameObjectsPool objectPool)
    {
        _pool = objectPool;
    }

    public void ReturnToPool()
    {
        _pool.ReturnToPool(this);
    }
}
