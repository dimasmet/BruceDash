using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGame : MonoBehaviour
{
    private GameObjectsPool _WallObjectsPool;
    private GameObjectsPool _treeBlocksObjectsPool;

    [SerializeField] private Transform _containerObjects;

    [SerializeField] private WallObject _prefabWall;
    [SerializeField] private TreeBlocks _prefabtreeBlocks;

    [SerializeField] private Transform _posSpawnWall;
    [SerializeField] private float _timeSpawnWall;

    [SerializeField] private Transform _posSpawnCircle;
    [SerializeField] private float _timeSpawnCircle;

    [SerializeField] private WallObject _WallDouble;
    [SerializeField] private WallObject _TreeDouble;
    [SerializeField] private WallObject _TreeThrow;
    [SerializeField] private WallObject _ThreeWall;
    [SerializeField] private WallObject _ThreeThorw2;
    [SerializeField] private MovingObject _CircleLevel;

    [SerializeField] private MovingObject _tableStep;
    [SerializeField] private TextMesh _textMesh;

    private int _countWall;

    public float TimeS;

    private void Awake()
    {
        _WallObjectsPool = new GameObjectsPool(_containerObjects, _prefabWall.GetComponent<ObjectGame>());
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

    public void BackToLevelSpeedTime()
    {
        Time.timeScale = GameMain.currentLevel.speedMove;
        TimeS = Time.timeScale;
    }

    public void StartGame()
    {
        _countWall = 0;

        _textMesh.text = "STEP " + GameMain.currentLevel.numberLevel;
        _tableStep.transform.position = _posSpawnWall.position;
        _tableStep.gameObject.SetActive(true);

        Time.timeScale = GameMain.currentLevel.speedMove;
        TimeS = Time.timeScale;

        StartCoroutine(WaitToSpawnNewWall());
        StartCoroutine(WaitToUpSpeed());
    }

    private IEnumerator WaitToUpSpeed()
    {
        while (Time.timeScale < GameMain.currentLevel.speedMove)
        {
            yield return new WaitForSeconds(0.5f / Time.timeScale);
            Time.timeScale += 0.05f;

            
        }

    }

    private IEnumerator WaitToSpawnNewWall()
    {
        while (true)
        {
            if (_countWall % 2 == 0)
            {
                int randomValue = Random.Range(0, 100);

                if (randomValue > 80)
                {
                    _WallDouble.transform.position = _posSpawnWall.position;
                    _WallDouble.gameObject.SetActive(true);
                }
                else
                {
                    if (randomValue <= 80 && randomValue >60)
                    {
                        _TreeDouble.transform.position = _posSpawnCircle.position;
                        _TreeDouble.gameObject.SetActive(true);
                        _TreeThrow.Init();
                    }
                    else
                    {
                        if (randomValue <= 60 && randomValue > 40)
                        {
                            _TreeThrow.transform.position = _posSpawnCircle.position;
                            _TreeThrow.gameObject.SetActive(true);
                            _TreeThrow.Init();
                        }
                        else
                        {
                            if (randomValue >= 40 && randomValue > 20)
                            {
                                _ThreeWall.transform.position = _posSpawnCircle.position;
                                _ThreeWall.gameObject.SetActive(true);
                                _ThreeWall.Init();
                            }
                            else
                            {
                                _ThreeThorw2.transform.position = _posSpawnCircle.position;
                                _ThreeThorw2.gameObject.SetActive(true);
                                _ThreeThorw2.Init();
                            }
                        }
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
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(_timeSpawnWall);
        }
    }

    private void SpawnCircleFinalLevel()
    {
        Vector2 pos = _posSpawnCircle.position;
        pos.x -= 15;

        _CircleLevel.transform.position = pos;
        _CircleLevel.gameObject.SetActive(true);
    }

    private void SpawnTreeBlocksBonus()
    {
        ObjectGame objTreeBlocks = _treeBlocksObjectsPool.GetElement();
        objTreeBlocks.ActiveStars();
        objTreeBlocks.transform.position = _posSpawnCircle.position;
    }
}
