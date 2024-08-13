using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGame : MonoBehaviour
{
    private GameObjectsPool _WallObjectsPool;
    private GameObjectsPool _CircleObjectsPool;

    [SerializeField] private Transform _containerObjects;

    [SerializeField] private WallObject _prefabWall;
    [SerializeField] private CircleLevel _prefabCircle;

    [SerializeField] private Transform _posSpawnWall;
    [SerializeField] private float _timeSpawnWall;

    [SerializeField] private Transform _posSpawnCircle;
    [SerializeField] private float _timeSpawnCircle;

    private void Start()
    {
        Time.timeScale = 2;

        _WallObjectsPool = new GameObjectsPool(_containerObjects, _prefabWall.GetComponent<ObjectGame>());
        _CircleObjectsPool = new GameObjectsPool(_containerObjects, _prefabCircle.GetComponent<ObjectGame>());

        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(WaitToSpawnNewWall());

        StartCoroutine(WaitToSpawnNewCircle());
    }

    private IEnumerator WaitToSpawnNewWall()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSpawnWall);

            ObjectGame objWall = _WallObjectsPool.GetElement();
            objWall.transform.position = _posSpawnWall.position;
        }
    }

    private IEnumerator WaitToSpawnNewCircle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSpawnWall);

            ObjectGame objWall = _CircleObjectsPool.GetElement();
            objWall.transform.position = _posSpawnCircle.position;
        }
    }
}
