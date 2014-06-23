using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GameData : MonoBehaviour {

    // Event
    public delegate void OnThemeChangeEvent(ColorTheme theme);
    public event OnThemeChangeEvent ThemeChange;

    public static event EventHandler VibrateMe;

    private Vector2 _oldScreenSize = Vector2.zero; 

    public bool ScreenResize = false; 

    public static void FireVibrateMe()
    {
        var handler = VibrateMe;
        if (handler != null) handler(null, EventArgs.Empty);
    }


    private static GameData _instance;

    public ColorTheme CurrenTheme = ColorTheme.Day;

    [HideInInspector]
    public List<Score> Score = new List<Score>();

    public int ThisGameScore = 0;
    public int ThisGameART = 0;
    public int ThisGameAC = 0;
    public int ThisGameStreak = 0;

    private static GameData _objInstance; 

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

    public List<Score> GetScore()
    {
        return Score; 
    }

    public void TryAddScore(Score newScore)
    {
        Score.Sort();
        Score.Add(newScore);
        Score.Sort();
        while (Score.Count > 5)
            Score.RemoveAt(0);
    }

    public void SaveScore()
    {
        Score.Sort();

        var points = new List<int>();
        var counts = new List<int>(); 
        var times = new List<int>();

        foreach (var score in Score)
        {
            points.Add(score.Points);
            counts.Add(score.Count);
            times.Add(score.Time);
        }

        PlayerPrefsX.SetIntArray("Points", points.ToArray());
        PlayerPrefsX.SetIntArray("Counts", counts.ToArray());
        PlayerPrefsX.SetIntArray("Times", times.ToArray());
    }

    public void LoadScore()
    {
       Score.Clear();
        for (var i = 0; i < PlayerPrefsX.GetIntArray("Points").Length; i++)
        {
            Score.Add(new Score(
                PlayerPrefsX.GetIntArray("Points")[i],
                PlayerPrefsX.GetIntArray("Counts")[i],
                PlayerPrefsX.GetIntArray("Times")[i]
                ));
        }
        Score.Sort();
    }

    protected virtual void OnThemeChange(ColorTheme theme)
    {
        var handler = ThemeChange;
        if (handler != null) handler(theme);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ChangeTheme(ColorTheme newTheme)
    {
        CurrenTheme = newTheme; 
        OnThemeChange(newTheme);
    }

	void Start ()
	{
	    OnThemeChange(CurrenTheme);
	}
	
	void Update ()
	{
	    if (ScreenResize)
	        ScreenResize = false;

	    if (Screen.width == _oldScreenSize.x && Screen.height == _oldScreenSize.y) return;
	    ScreenResize = true; 
	    _oldScreenSize = new Vector2(Screen.width, Screen.height);
	}
}
