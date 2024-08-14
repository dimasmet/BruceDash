using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGame : MonoBehaviour
{
    private GameObjectsPool _WallObjectsPool;
    private GameObjectsPool _CircleObjectsPool;
    private GameObjectsPool _treeBlocksObjectsPool;

    [SerializeField] private Transform _containerObjects;

    [SerializeField] private WallObject _prefabWall;
    [SerializeField] private CircleLevel _prefabCircle;
    [SerializeField] private TreeBlocks _prefabtreeBlocks;

    [SerializeField] private Transform _posSpawnWall;
    [SerializeField] private float _timeSpawnWall;

    [SerializeField] private Transform _posSpawnCircle;
    [SerializeField] private float _timeSpawnCircle;

    [SerializeField] private MovingObject _WallDouble;
    [SerializeField] private MovingObject _TreeDouble;
    [SerializeField] private MovingObject _TreeThrow;

    private int _countWall;

    private void Awake()
    {
        _WallObjectsPool = new GameObjectsPool(_containerObjects, _prefabWall.GetComponent<ObjectGame>());
        _CircleObjectsPool = new GameObjectsPool(_containerObjects, _prefabCircle.GetComponent<ObjectGame>());
        _treeBlocksObjectsPool = new GameObjectsPool(_containerObjects, _prefabtreeBlocks.GetComponent<ObjectGame>());
    }

    private void Start()
    {
        GameMain.OnStartGame += StartGame;
        GameMain.OnEndGame += StopGame;
    }

    private void OnDestroy()
    {
        GameMain.OnStartGame -= StartGame;
        GameMain.OnEndGame -= StopGame;
    }

    private void StopGame()
    {
        StopAllCoroutines();
    }

    public void StartGame()
    {
        _countWall = 0;
        Time.timeScale = 1.5f;

        Debug.Log("Spawn NEw");

        StartCoroutine(WaitToSpawnNewWall());
        StartCoroutine(WaitToUpSpeed());
    }

    private IEnumerator WaitToUpSpeed()
    {
        while (Time.timeScale < GameMain.currentLevel.speedMove)
        {
            yield return new WaitForSeconds(0.5f);
            Time.timeScale += 0.05f;
        }
    }

    private IEnumerator WaitToSpawnNewWall()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSpawnWall);

            if (_countWall % 2 == 0)
            {
                int randomValue = Random.Range(0, 100);

                if (randomValue > 70)
                {
                    _WallDouble.transform.position = _posSpawnWall.position;
                    _WallDouble.gameObject.SetActive(true);
                }
                else
                {
                    if (randomValue <= 70 && randomValue >30)
                    {
                        _TreeDouble.transform.position = _posSpawnCircle.position;
                        _TreeDouble.gameObject.SetActive(true);
                    }
                    else
                    {
                        _TreeThrow.transform.position = _posSpawnCircle.position;
                        _TreeThrow.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                ObjectGame objWall = _WallObjectsPool.GetElement();
                objWall.transform.position = _posSpawnWall.position;
            }

            _countWall++;

            if (_countWall == GameMain.currentLevel.countStep)
            {
                _countWall = 0;
                SpawnCircleFinalLevel();
                Invoke(nameof(SpawnTreeBlocksBonus),2f);
                Debug.Log("Next Level Circle");
                StopAllCoroutines();
            }
        }
    }

    private void SpawnCircleFinalLevel()
    {
        ObjectGame objWall = _CircleObjectsPool.GetElement();
        Vector2 pos = _posSpawnCircle.position;
        pos.x -= 4;

        objWall.transform.position = pos;
    }

    private void SpawnTreeBlocksBonus()
    {
        ObjectGame objTreeBlocks = _treeBlocksObjectsPool.GetElement();
        objTreeBlocks.transform.position = _posSpawnCircle.position;
    }
}
