using UnityEngine;
using System.Collections;

public class MuteSound : ButtonTask
{

    public Sprite SoundOn;
    public Sprite SoundOff;

    private SpriteRenderer _renderer; 

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    public override void Activate()
    {
        GameData.Instance.Mute = !GameData.Instance.Mute;
        UpdateSprite();
    }


    private void UpdateSprite()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = !GameData.Instance.Mute
            ? SoundOff
            : SoundOn;
    }
}
