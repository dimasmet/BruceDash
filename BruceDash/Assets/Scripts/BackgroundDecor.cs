using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDecor : MonoBehaviour
{
    [SerializeField] private DecordObj[] treeObjs;
    [SerializeField] private DecordObj[] cloudObjs;

    [SerializeField] private Transform _posToSpawn;

    private void Start()
    {
        for (int i = 0; i < treeObjs.Length; i++)
        {
            treeObjs[i].Init(this);
        }

        for (int i = 0; i < cloudObjs.Length; i++)
        {
            cloudObjs[i].Init(this);
        }
        GameMain.OnStartGame += ActiveDecor;
    }

    private void OnDestroy()
    {
        GameMain.OnStartGame -= ActiveDecor;
    }

    private void ActiveDecor()
    {
        for (int i = 0; i < treeObjs.Length; i++)
        {
            treeObjs[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < cloudObjs.Length; i++)
        {
            cloudObjs[i].gameObject.SetActive(true);
        }
    }

    public void ReturnDecor(DecordObj decor)
    {
        decor.transform.position = _posToSpawn.position;
    }
}
