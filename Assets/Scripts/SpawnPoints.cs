﻿using UnityEngine;
using System.Collections;

public class SpawnPoints : MonoBehaviour
{
    public TextMesh PointText;
    public Color PlusColor, MinusColor, GoldColor;
    public int SortingLayer;

    private ColorTheme _activeColor = ColorTheme.Day; 

    void Awake()
    {
        GameData.Instance.ThemeChange += SwitchTheme; 
    }

    public void AddPoints(int amount, Vector2 position)
    {
        var spawnedText = Instantiate(PointText, position, PointText.transform.rotation) as TextMesh;
        if (spawnedText == null) return;
        spawnedText.text = (string.Format("{0}{1}{2}", amount > 0 ? "+" : "", amount, "p"));
        if (amount > 0)
        {
            spawnedText.color = _activeColor == ColorTheme.Day ? PlusColor : GoldColor; 
        }
        else
        {
            spawnedText.color = _activeColor == ColorTheme.Day ? MinusColor : PlusColor; 
        }
        spawnedText.renderer.sortingOrder = SortingLayer; 
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
