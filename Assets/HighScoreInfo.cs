using System.Globalization;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class HighScoreInfo : MonoBehaviour
{

    public StatType StatToShow;
    public int NumberRank;
    private NumberFormatInfo _nfi; 
    private TextMesh _text;

    void Awake()
    {
        _nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        _nfi.NumberGroupSeparator = " ";
    }

	// Use this for initialization
	void Start ()
	{
	    _text = GetComponent<TextMesh>();
	    UpdateStat();
	}
	
	// Update is called once per frame
	public void UpdateStat ()
	{

        GameData.Instance.LoadScore();
        var index = GameData.Instance.Score.Count - NumberRank; 
	    if (index > GameData.Instance.Score.Count || index < 0)
	    {
	        _text.text = "---"; 
            return;
	    }
	    var score = GameData.Instance.Score[index]; 
	    switch (StatToShow)
	    {
            case StatType.Score:
                _text.text = string.Format("{0}{1}", score.Points.ToString("n0", _nfi), "p"); 
	            break; 
            case StatType.ArrowsCleard:
                _text.text = string.Format("{0}", score.Count.ToString("n0", _nfi));
	            break; 
            case StatType.AvgReactionTime:
                _text.text = string.Format("{0}{1}", score.Time.ToString("n0", _nfi), "ms");
	            break; 
	    }
	}
}
