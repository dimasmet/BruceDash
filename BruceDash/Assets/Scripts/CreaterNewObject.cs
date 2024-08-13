using UnityEngine;

public class CreaterNewObject : MonoBehaviour
{
    public static CreaterNewObject Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject CreateObject(GameObject prefabGameObject)
    {
        GameObject go = Instantiate(prefabGameObject);
        return go;
    }
}
