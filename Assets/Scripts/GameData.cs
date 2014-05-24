using UnityEngine;

public class GameData : MonoBehaviour {

    public delegate void OnThemeChangeEvent(ColorTheme theme);

    public event OnThemeChangeEvent ThemeChange;

    private static GameData _instance;

    public ColorTheme CurrenTheme = ColorTheme.Day;

    public static GameData Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<GameData>();
            if (_instance != null) return _instance;
            Debug.Log("Missing GameData!");
            return _instance;
        }
    }

    protected virtual void OnThemeChange(ColorTheme theme)
    {
        var handler = ThemeChange;
        if (handler != null) handler(theme);
    }
    public float SwipeThreshold = 80;

    void Awake ()
    {
    }

	void Start ()
	{
	    ThemeChange(CurrenTheme);
	}
	
	void Update () {
	
	}
}
