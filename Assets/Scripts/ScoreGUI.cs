using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour
{
    public TextMesh HiScore; 
    public TextMesh Score;

    private float _hiScore;
    private float _score = 0; 

	void Start () {
	    WriteScore(_score);
	    _hiScore = PlayerPrefs.GetFloat("HiScore");
        WriteHiScore(_hiScore);
	}

    public void NewGame()
    {
        _score = 0; 
    }

    public void AddScore(int score)
    {
        _score += score; 
        WriteScore(_score);

        if (!(_score > _hiScore)) return;
        AddNewHiScore(_score);
        WriteHiScore(_hiScore);
    }

    public void AddNewHiScore(float hiscore)
    {
        _hiScore = hiscore; 
        PlayerPrefs.SetFloat("HiScore", hiscore);
        PlayerPrefs.Save();
    }

    public void WriteScore(float score)
    {
        Score.text = string.Format("{0} {1}", "Score:", score);
    }

    public void WriteHiScore(float hiscore)
    {
        HiScore.text = string.Format("{0} {1}", "Hi-Score:", hiscore);
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
