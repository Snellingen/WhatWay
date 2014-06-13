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
        AudioListener.pause = !AudioListener.pause; 
        UpdateSprite();
    }


    private void UpdateSprite()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = !AudioListener.pause
            ? SoundOff
            : SoundOn;
    }
}
