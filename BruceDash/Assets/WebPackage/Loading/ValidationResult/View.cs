using UnityEngine;

public class View : ValidationResult
{
    public const string SavedResultKey = "Result";

    private readonly string _url;
    private readonly RectTransform _viewPanel;
    private readonly GameObject _viewGameObject;

    public View(string url, RectTransform viewPanel)
    {
        _url = PlayerPrefs.GetString(SavedResultKey, url);
        _viewPanel = viewPanel;

        Screen.orientation = ScreenOrientation.Portrait;

        _viewGameObject = new GameObject("RecordsPanel");
        _viewGameObject.AddComponent<UniWebView>();
    }

    public override void Run()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

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

    }
}