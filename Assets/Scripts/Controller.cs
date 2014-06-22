using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Spawn))]
[RequireComponent(typeof(SpawnPoints))]
public class Controller : MonoBehaviour
{
    private Spawn _spawner;
    private SpawnPoints _pointSpawner;

    public float SecondsPerSpawn;
    private float _timer;

    public CountDown CountDownTime; 

    public AudioSource PositiveSound;
    public AudioSource NegativeSound;
    public AudioSource NeutralSound;

    public GameObject GameOverScreen;

    private ScoreGUI _scoreHandler;

    private SceneFade _sceneFade;

    private int _spawnSize = 1;
    private int _currentProgress = 0;
    private int _nextLevel = 10;
    private int _numCleared = 0; 
    private int _streak = 0;
    private int _maxStreak = 0;
    private readonly List<float> _reactionTimes = new List<float>(); 

    public bool Cheat = false;

    private bool _firstStrike = true; 

    [HideInInspector] 
    public float ThisGameScore = 0; 
    private bool _pause = false;

    void Awake()
    {
        NewGame();
    }

    void Start()
    {
        _spawner = GetComponent<Spawn>();
        _pointSpawner = GetComponent<SpawnPoints>();
        _scoreHandler = GetComponent<ScoreGUI>();
        _sceneFade = FindObjectOfType<SceneFade>();
        if (_sceneFade != null)
            _sceneFade.FadeOut();
        InputManager.Instance.Swipe += OnSwipe;

        if (PositiveSound != null)
            PositiveSound = GameObject.Find("Positive").GetComponent<AudioSource>();
        if (NeutralSound != null)
            NeutralSound = GameObject.Find("Netural").GetComponent<AudioSource>();
        if (NegativeSound != null)
            NegativeSound = GameObject.Find("Negative").GetComponent<AudioSource>(); 
    }

    void Update()
    {
        if (_pause) return;
        if (_spawner == null) return;
        _timer += Time.deltaTime;
    }

    public void NewGame()
    {

        CountDownTime.ResetClock();
        _firstStrike = true; 
        _reactionTimes.Clear();
        _streak = 0;
        _maxStreak = 0;
        _currentProgress = 0;
        _spawnSize = 1;
        _numCleared = 0;
        _nextLevel = 10;

        if (_scoreHandler != null)
            _scoreHandler.NewGame();
        if (_spawner != null)
            _spawner.StartGame();
    }

    public void SpawnNewArrows()
    {
        if (_currentProgress >= _nextLevel && _spawnSize < 7)
        {
            _spawnSize += 2;
            _nextLevel *= 3;
            _currentProgress = 0;
        }
        _spawner.SpawnArrows((SpawnType)Random.Range(0, 4), (SpawnRotation)Random.Range(0, 4), _spawnSize);
        _timer -= SecondsPerSpawn;
        _currentProgress++;
    }

    public void Clear()
    {
        CountDownTime.ResetClock();
        _reactionTimes.Clear();
        _streak = 0;
        _maxStreak = 0;
        _currentProgress = 0;
        _spawnSize = 1;
        _numCleared = 0; 
        _spawner.Clear();
        _scoreHandler.ClearAndUpdate();
    }

    public void OnSwipe(SwipeDirection dir)
    {
        if (_pause) return;

        if (_firstStrike)
        {
            CountDownTime.StartClock();
            _firstStrike = false; 
        }

        if (CountDownTime.TimeOut)
            BadMove();

        if ((int)dir == (int)_spawner.LastSpawRotation || Cheat)
            GoodMove();
        else
            BadMove();
        
    }

    private void GoodMove()
    {
        if (_timer < 1.2)
        {
            _streak++;
            if (PositiveSound.pitch < 2)
                PositiveSound.pitch += 0.01f;
        }
        else
        {
            if (_streak > _maxStreak)
                _maxStreak = _streak;
            _streak = 1;
            PositiveSound.pitch = 1;
        }

        _numCleared++;
        PositiveSound.Play();

        if (_scoreHandler != null)
        {
            var score = _scoreHandler.CalucalteScore(_timer, _streak, _spawnSize);
            _reactionTimes.Add(_timer);
            _pointSpawner.AddPoints(score, _spawner.LastPosition, _streak > 1);
            _scoreHandler.AddScore(score);

        }
        _timer = 0;
        _spawner.AnimeSpawn();
        SpawnNewArrows();
    }

    private void BadMove()
    {
        if (_streak > _maxStreak)
            _maxStreak = _streak;

        NegativeSound.Play();
        _pointSpawner.AddPoints((int)-_scoreHandler.GetScore(), _spawner.LastPosition);
        GameData.FireVibrateMe();

        if (_reactionTimes.Count > 1)
            _reactionTimes.RemoveAt(0);

        GameData.Instance.ThisGameScore = _scoreHandler.GetScore();
        GameData.Instance.ThisGameART = _reactionTimes.Count > 0 ? _reactionTimes.Average() * 1000 : 0;
        GameData.Instance.ThisGameStreak = _maxStreak;
        GameData.Instance.ThisGameAC = _numCleared;

        GameData.Instance.SaveScore();

        if (GameOverScreen != null)
        {
            _scoreHandler.AddScoreToList();
            GameOverScreen.SetActive(true);
            SetPause(true);

        }

        Clear();

        PositiveSound.pitch = 1f;
        _timer = 0;
    }

    public void SetPause(bool value)
    {
        _pause = value;
        CountDownTime.SetPause(value);
    }

    void OnDestroy()
    {
        if (InputManager.Instance != null) 
        InputManager.Instance.Swipe -= OnSwipe;
    }
}
