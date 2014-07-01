using System.Linq;
using UnityEngine;

public class SceneFade : MonoBehaviour
{

    public delegate void FadeInDoneEvent();

    public event FadeInDoneEvent FadeInDone;

    protected virtual void OnFadeInDone()
    {
        var handler = FadeInDone;
        if (handler == null) return;
        handler();
    }

    public float fadeSpeed = 0.8f;
    [HideInInspector]

    private bool _fadeOut;

    private bool _fadeIn;

    private float _preFrameTime = 0;
    private float _newFrameTime = 0;
    private float _deltaTime;

    private Color _color;

    private bool _introFade = true;

    void Start()
    {

        _fadeOut = true;
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        DontDestroyOnLoad(this);
        _newFrameTime = Time.realtimeSinceStartup;
        _color = guiTexture.color;
    }

    void Update()
    {
        _preFrameTime = _newFrameTime;
        _newFrameTime = Time.realtimeSinceStartup;

        _deltaTime = _newFrameTime - _preFrameTime;

        if (_fadeOut)
        {
            if (_introFade)
            {
                guiTexture.color = Color.black;
                _introFade = false;
            }
            FadeOut();
        }

        if (_fadeIn)
        {
            FadeIn();
        }


    }

    public void FadeOut()
    {
        _fadeOut = true;
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * _deltaTime);
        if (!(guiTexture.color.a <= 0.01f)) return;
        guiTexture.color = Color.clear;
        _fadeOut = false;
    }

    public void FadeIn()
    {
        _fadeIn = true;
        guiTexture.color = Color.Lerp(guiTexture.color, new Color(
            _color.r,
            _color.g,
            _color.b,
           1), fadeSpeed * _deltaTime);

        if (!(guiTexture.color.a >= 0.5)) return;
        _fadeIn = false;
        OnFadeInDone();
    }

    public void StartScene()
    {
    }
}
