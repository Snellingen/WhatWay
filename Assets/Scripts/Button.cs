
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour
{
    public Color ClickColor;
    private SpriteRenderer _srender;
    private Color _originalColor;
    private bool _active = false;
    public AudioSource ButtonSound;

    public ButtonTask Action; 

    void Start()
    {
        _srender = GetComponent<SpriteRenderer>();
        InputManager.Instance.Tap += OnButtonClick;
        _originalColor = _srender.color;
    }

    private void OnButtonClick(Vector2 tappos, TapState tapState)
    {
        var intersect = CheckIntersect(tappos);
        if (intersect)
        {
            if (tapState == TapState.Down)
            { 
                if (!_active)
                {
                    _originalColor = _srender.color;
                    _srender.color = ClickColor;
                    if (ButtonSound != null)
                        ButtonSound.Play();
                }
                _active = true;
            }
            else
            {
                OnButtonRelease();
                Activate();
                _active = false;
            }
        }else if (_active)
            OnButtonRelease();
    }

    public void OnButtonRelease()
    {
        _srender.color = _originalColor;
        _active = false; 
    }

    public void Activate()
    {
        Action.Activate();
    }

    private bool CheckIntersect(Vector3 tappos)
    {
        var wp = Camera.main.ScreenToWorldPoint(tappos);
        var touchPos = new Vector2(wp.x, wp.y);
        return (collider2D == Physics2D.OverlapPoint(touchPos));
    }

    void OnDestroy()
    {
        if (InputManager.Instance != null)
        InputManager.Instance.Tap -= OnButtonClick;
    }
}