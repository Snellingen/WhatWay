using UnityEngine;
using System.Collections;

public class ClearHS : ButtonTask
{
    public HighScoreInfo[] ToClear;

    public override void Activate()
    {
        PlayerPrefs.DeleteAll();

        foreach (var highScoreInfo in ToClear)
        {
            highScoreInfo.UpdateStat();
        }
    }
}
