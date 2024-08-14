using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public static GameMain Instance;

    public static Action OnCircleLevelSuccess;
    public static Action OnCircleLevelOvercome;

    public static Action OnStartGame;
    public static Action OnEndGame;

    [SerializeField] private LevelData[] _levelsData;

    public static LevelData currentLevel;
    private int _numberCurrentLevel;

    [SerializeField] private SpawnerGame _spawnerGame;

    private bool isOvercomeCircleLevel = false;

    public static BalancePlayer BalancePlayer;

    [SerializeField] private Button _startBtn;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        _startBtn.onClick.AddListener(() =>
        {
            OnStartGame?.Invoke();
        });
    }

    private void Start()
    {
        OnCircleLevelSuccess += CircleLevelSuccess;
        OnCircleLevelOvercome += CheckSuccessCheckLevel;

        currentLevel = _levelsData[_numberCurrentLevel];

        isOvercomeCircleLevel = false;

        //_balancePlayer = new BalancePlayer();
    }

    private void OnDestroy()
    {
        OnCircleLevelSuccess -= CircleLevelSuccess;
        OnCircleLevelOvercome -= CheckSuccessCheckLevel;
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

            GameMain.OnStartGame?.Invoke();
        }
        else
            Debug.Log("end game");
    }
}
