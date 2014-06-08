using System.Linq;
using UnityEngine;

public class ScoreGUI : MonoBehaviour
{
    public TextMesh HiScore; 
    public TextMesh Score;

    private float _score = 0; 

	void Start () {
	    WriteScore();
	    GameData.Instance.LoadScore();
	    WriteHiScore(GameData.Instance.Score.Count == 0 ? 0 : GameData.Instance.Score.Last());
	}

    public void NewGame()
    {
        _score = 0; 
    }

    public void AddScore(float score)
    {
        _score = score; 
        WriteScore();
    }

    public void ClearAndUpdate()
    {
        _score = 0; 
        WriteScore();
        WriteHiScore(GameData.Instance.Score.Count == 0 ? 0 : GameData.Instance.Score.Last());
    }

    public void AddScoreToList()
    {
        GameData.Instance.Score.Add(_score);
    }

    public float GetScore()
    {
        return _score; 
    }
    public void WriteScore()
    {
        Score.text = string.Format("{0} {1}", "Score:", _score);
    }

    public void WriteHiScore(float hiscore)
    {
        HiScore.text = string.Format("{0} {1}", "Best Score:", hiscore);
    }

    public int CalucalteScore(float time, int streak, int cntArrows)
    {
        var timeBonus = 10000 / (time * 1000);
        if (timeBonus > 100 || timeBonus <= 0) timeBonus = 100;
        var streakBonus = streak * cntArrows;
        var total = timeBonus * streakBonus;
        //Debug.Log("Timebonus: " + timeBonus + " StreakBonus: " + streakBonus + " Total =" + total);

        return (int) total;
    }
}
