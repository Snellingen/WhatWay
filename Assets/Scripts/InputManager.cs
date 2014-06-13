using System;
using UnityEngine;

public enum SwipeDirection
{
    Right,
    Left,
    Up,
    Down,
    NONE
}

public enum TapState
{
    Down,
    Tapped
}

public class InputManager : MonoBehaviour
{
    public delegate void OnSwipeEvent(SwipeDirection dir);
    public event OnSwipeEvent Swipe;

    public string BackButtonLoadScene = "Menu"; 
    protected virtual void OnSwipe(SwipeDirection dir)
    {
        var handler = Swipe;
        if (handler != null) handler(dir);
    }

    public delegate void OnTapEvent(Vector2 tapPos, TapState tapState);
    public event OnTapEvent Tap;

    protected virtual void OnTap(Vector2 tappos, TapState tapState)
    {
        var handler = Tap;
        if (handler != null) handler(tappos, tapState);
    }

    public Transform Trail;
    public float SwipeThreshold = 80;

    private bool _mouseDownLastFrame = false;
    private bool _isTouchInput = true;
    private Vector2 _firstPressPos, _secondPressPos, _currentSwipe;

#if UNITY_IPHONE
    private const float TOLERANCE = 40;
#else
    private const float TOLERANCE = 5;
#endif

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

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Trail == null)
            Trail = GameObject.FindGameObjectWithTag("Trail").transform;

        CheckTouch();
        if (!_isTouchInput)
            CheckMouse();
        DrawTrail();
        CheckKeyboard();
    }


    public void CheckTouch()
    {
        if (Input.touches.Length <= 0)
        {
            _isTouchInput = false; 
            return;
        }
        _isTouchInput = true; 
        var t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began)
        {
            _firstPressPos = new Vector2(t.position.x, t.position.y);
            OnTap(_firstPressPos, TapState.Down);
        }
        if (t.phase == TouchPhase.Moved)
            OnTap(new Vector2(t.position.x, t.position.y), TapState.Down);

        if (t.phase != TouchPhase.Ended) return;
        _secondPressPos = new Vector2(t.position.x, t.position.y);
        _currentSwipe = new Vector3(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);

        if (_currentSwipe.sqrMagnitude < TOLERANCE)
        {
            OnTap(_firstPressPos, TapState.Tapped);
        }
        else
        {
            var dir = CheckSwipe(_currentSwipe);
            if (dir != SwipeDirection.NONE)
                OnSwipe(dir);
        }
    }

    private static SwipeDirection CheckSwipe(Vector3 swipeVector)
    {
        swipeVector.Normalize();
        if (swipeVector.y > 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f)
        {
            return SwipeDirection.Up;
        }
        if (swipeVector.y < 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f)
        {
            return SwipeDirection.Down;
        }
        if (swipeVector.x < 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f)
        {
            return SwipeDirection.Left;
        }
        if (swipeVector.x > 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f)
        {
            return SwipeDirection.Right;
        }
        return SwipeDirection.NONE;
    }

    private void DrawTrail()
    {
        if (Input.touches.Length > 0)
        {
            var t = Input.GetTouch(0);
            if (t.phase != TouchPhase.Began && t.phase != TouchPhase.Moved) return;
            var pos = Camera.main.ScreenToWorldPoint(t.position);
            if (Trail != null)
            Trail.position = new Vector3(pos.x, pos.y, 2);
        }
        else
        {
            if (!Input.GetMouseButton(0)) return;
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Trail != null)
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

        if (!Input.GetKey(KeyCode.Escape)) return;
        if (Application.loadedLevelName == BackButtonLoadScene)
            Application.Quit();
        else Application.LoadLevel(BackButtonLoadScene);
    }

    public void CheckMouse()
    {
        if (Input.GetMouseButton(0))
        {
            OnTap(Input.mousePosition, TapState.Down);
            _mouseDownLastFrame = true; 
            return;
        }
        if (!Input.GetMouseButtonUp(0) || !_mouseDownLastFrame) return;
        OnTap(Input.mousePosition, TapState.Tapped);
        _mouseDownLastFrame = false;
    }
}
