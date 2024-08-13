using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool
{
    private Transform _container;
    private ObjectGame _prefabs;

    private Stack<ObjectGame> _objectPool = new Stack<ObjectGame>();
    private List<ObjectGame> _listElements = new List<ObjectGame>();

    public GameObjectsPool(Transform container, ObjectGame prefabObject)
    {
        _container = container;
        _prefabs = prefabObject;
    }

    public ObjectGame GetElement()
    {
        ObjectGame element;

        if (_objectPool.Count > 0)
        {
            element = _objectPool.Pop();
            element.transform.SetParent(null);
            element.gameObject.SetActive(true);
        }
        else
        {
            element = CreaterNewObject.Instance.CreateObject(_prefabs.gameObject).GetComponent<ObjectGame>();
            element.SetObjectPool(this);

            _listElements.Add(element);
        }

        return element;
    }

    public void ReturnToPool(ObjectGame element)
    {
        element.gameObject.SetActive(false);
        element.transform.SetParent(_container);

        element.transform.localPosition = Vector3.zero;
        element.transform.localScale = new Vector3(1, 1, 1);
        element.transform.localEulerAngles = Vector3.one;

        _objectPool.Push(element);
    }

    public void HideAllObject()
    {
        for (int i = 0; i < _listElements.Count; i++)
        {
            _listElements[i].gameObject.SetActive(false);
        }
    }
}
