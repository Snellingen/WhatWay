using System;
using UnityEngine;

public enum SwipeDirection
{
    Right,
    Left,
    Up,
    Down
}

public class InputManager : MonoBehaviour
{
    public delegate void OnSwipeEvent(SwipeDirection dir);
    public event OnSwipeEvent OnSwipe;

    public Transform Trail; 
    public float SwipeThreshold = 80; 

    private Vector2 _firstPressPos, _secondPressPos, _currentSwipe; 

    protected virtual void OnOnSwipe(SwipeDirection dir)
    {
        var handler = OnSwipe;
        if (handler != null) handler(dir);
    }

    private static InputManager _instance;

    private InputManager() { }

    public static InputManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<InputManager>();
            if (_instance != null) return _instance;
            Debug.Log("Missing InputManager!");
            return _instance;
        }
    }

    void Update()
    {
       CheckTouch();
       DrawTrail();
       CheckKeyboard();
    }

    public void CheckTouch()
    {
        if (Input.touches.Length <= 0) return;
        var t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began)
        {
            _firstPressPos = new Vector2(t.position.x, t.position.y);
        }
        if (t.phase != TouchPhase.Ended) return;
        _secondPressPos = new Vector2(t.position.x, t.position.y);
        _currentSwipe = new Vector3(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);
        _currentSwipe.Normalize();
        if (_currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
        {
            OnSwipe(SwipeDirection.Up);
        }
        if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
        {
            OnSwipe(SwipeDirection.Down);
        }
        if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
        {
            OnSwipe(SwipeDirection.Left);
        }
        if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
        {
            OnSwipe(SwipeDirection.Right);
        }
    }

    private void DrawTrail()
    {
        if (Input.touches.Length > 0)
        {
            var t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved)
            {
                var pos = Camera.main.ScreenToWorldPoint(t.position);
                Trail.position = new Vector3(pos.x, pos.y, 2);
            }
        }
        else
        {
            if (!Input.GetMouseButton(0)) return;
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Trail.position = new Vector3(pos.x, pos.y, 2);
        }
    }

    public void CheckKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnSwipe(SwipeDirection.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSwipe(SwipeDirection.Right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnSwipe(SwipeDirection.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnSwipe(SwipeDirection.Down);
        }
    }
}
