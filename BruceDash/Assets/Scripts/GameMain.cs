using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public static GameMain Instance;

    public static BalancePlayer BalancePlayer;

    // Actions game
    public static Action OnCircleLevelSuccess;
    public static Action OnCircleLevelOvercome;

    public static Action OnClearScene;
    public static Action OnStartGame;
    public static Action OnEndGame;
    public static Action OnResultGame;
    //
    [SerializeField] private LevelData[] _levelsData;

    public static LevelData currentLevel;
    private int _numberCurrentLevel;

    [SerializeField] private SpawnerGame _spawnerGame;

    private bool isOvercomeCircleLevel = false;

    [SerializeField] private TimeLevel _timeLevelHandler;

    [Header("Result view control")]
    [SerializeField] private ResultLevelView _resultLevelView;
    //Records
    private RecordPlayerInGame _recordPlayerInGame;
    [SerializeField] private Text _recordTextField;

    private void Awake()
    {
        Application.targetFrameRate = 90;

        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        OnCircleLevelSuccess += CircleLevelSuccess;
        OnCircleLevelOvercome += CheckSuccessCheckLevel;
        OnStartGame += RunGame;
        OnEndGame += StopGame;
        OnResultGame += OpenResultLevel;

        _recordPlayerInGame = new RecordPlayerInGame(_recordTextField);

        currentLevel = _levelsData[_numberCurrentLevel];

        isOvercomeCircleLevel = false;
    }

    private void OnDestroy()
    {
        OnCircleLevelSuccess -= CircleLevelSuccess;
        OnCircleLevelOvercome -= CheckSuccessCheckLevel;
        OnStartGame -= RunGame;
        OnEndGame -= StopGame;
        OnResultGame -= OpenResultLevel;
    }

    public void FirstStartLevel()
    {
        Time.timeScale = 1.5f;
        _numberCurrentLevel = 0;
        OnClearScene?.Invoke();
        OnStartGame?.Invoke();
        _timeLevelHandler.StartTime();
    }

    private void RunGame()
    {
        currentLevel = _levelsData[0];
        MovingObject.ISMove = true;
    }

    private void StopGame()
    {
        _timeLevelHandler.GetResultTime();
        MovingObject.ISMove = false;
    }

    private void OpenResultLevel()
    {
        float timeResult = _timeLevelHandler.GetResultTime();
        bool isRecord = _recordPlayerInGame.CheckNewResult(timeResult);

        _resultLevelView.ShowResultGame(_timeLevelHandler.GetTimeResultString(), isRecord);
    }

    private void CircleLevelSuccess()
    {
        isOvercomeCircleLevel = true;
    }

    private void CheckSuccessCheckLevel()
    {
        if (isOvercomeCircleLevel)
        {
            isOvercomeCircleLevel = false;

            _numberCurrentLevel++;
            currentLevel = _levelsData[_numberCurrentLevel];

            OnStartGame?.Invoke();
        }
        else
        {
            Debug.Log("Game Over");

            OnEndGame?.Invoke();
            OnResultGame?.Invoke();
        }
    }
}
