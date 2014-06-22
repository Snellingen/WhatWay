using System.Globalization;
using UnityEngine;
using System.Collections;

public enum StatType
{
    Score, 
    AvgReactionTime,
    Streak,
    ArrowsCleard
}

public class ShowStat : MonoBehaviour
{

    public StatType TypeOfScore;
    private TextMesh _text;
    private NumberFormatInfo _nfi;

	void Awake ()
	{

        _nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        _nfi.NumberGroupSeparator = " ";

	    _text = GetComponent<TextMesh>();
	    UpdateScore();
	}

    void UpdateScore()
    {
        switch (TypeOfScore)
        {
            case StatType.Score:
                _text.text = string.Format("{0}{1}", GameData.Instance.ThisGameScore.ToString("n0", _nfi), "p"); 
                break;
            case StatType.AvgReactionTime:
                _text.text = GameData.Instance.ThisGameART < 5000
                    ? GameData.Instance.ThisGameART.ToString("0") + "ms"
                    : "SLOW!"; 
                break;
            case StatType.Streak:
                _text.text = GameData.Instance.ThisGameStreak.ToString();
                break;
            case StatType.ArrowsCleard:
                _text.text = GameData.Instance.ThisGameAC.ToString(); 
                break;
        }
    }

	void Update () 
    {
        UpdateScore();
	}
}
