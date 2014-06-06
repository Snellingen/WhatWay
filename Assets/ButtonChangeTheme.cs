using UnityEngine;
using System.Collections;

public class ButtonChangeTheme : ButtonTask
{

    public Sprite DaySprite;
    public Sprite NightSprite;

    private SpriteRenderer _renderer; 

    void Start()
    {
       _renderer = GetComponent<SpriteRenderer>();
       UpdateSprite();
    }

    public override void Activate()
    {
        GameData.Instance.ChangeTheme(GameData.Instance.CurrenTheme == ColorTheme.Day
            ? ColorTheme.Night
            : ColorTheme.Day);

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        
        _renderer.sprite = GameData.Instance.CurrenTheme == ColorTheme.Day
            ? NightSprite
            : DaySprite;
    }
}
