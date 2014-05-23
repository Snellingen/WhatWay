using UnityEngine;
using System.Collections;

public class SpawnPoints : MonoBehaviour
{
    public TextMesh PointText;
    public Color PlusColor, MinusColor, GoldColor;

    private ColorTheme _activeColor = ColorTheme.Day; 

    void Awake()
    {
        GameData.Instance.ThemeChange += SwitchTheme; 
    }

    public void AddPoints(int amount, Vector2 position, bool streak = false)
    {
        var spawnedText = Instantiate(PointText, position, PointText.transform.rotation) as TextMesh;
        if (spawnedText == null) return;
        spawnedText.text = (string.Format("{0}{1}{2}", amount > 0 ? "+" : "", amount, "p"));
        if (amount > 0)
        {
            spawnedText.color = streak ? PlusColor : GoldColor; 
        }
        else
        {
            spawnedText.color = MinusColor; 
        }
        Destroy(spawnedText.gameObject, 1);
    }

    public void SwitchTheme(ColorTheme theme)
    {
        if (theme == ColorTheme.Day || theme == ColorTheme.Night)
        {
            _activeColor = theme; 
        }
    }

}
