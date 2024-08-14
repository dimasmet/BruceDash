using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{
    public void CollectCoin()
    {
        Destroy(gameObject);
    }
}
