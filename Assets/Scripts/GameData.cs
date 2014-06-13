using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class GameData : MonoBehaviour {

    public delegate void OnThemeChangeEvent(ColorTheme theme);

    public event OnThemeChangeEvent ThemeChange;

    private static GameData _instance;

    public ColorTheme CurrenTheme = ColorTheme.Day;

    [HideInInspector]
    public List<float> Score = new List<float>();

    public float ThisGameScore = 0;
    public float ThisGameART = 0;
    public float ThisGameAC = 0;
    public float ThisGameStreak = 0;

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

    public List<float> GetScore()
    {
        return Score; 
    }

    public void TryAddScore(float newScore)
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
        PlayerPrefsX.SetFloatArray("Score", Score.ToArray());
    }

    public void LoadScore()
    {
       Score.Clear();
        foreach (var score in PlayerPrefsX.GetFloatArray("Score"))
        {
            Score.Add(score);
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
	
	void Update () {
	
	}
}
