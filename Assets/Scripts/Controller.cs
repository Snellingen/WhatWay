using UnityEngine;

[RequireComponent(typeof(Spawn))]
[RequireComponent(typeof(SpawnPoints))]
public class Controller : MonoBehaviour
{
    private Spawn _spawner;
    private SpawnPoints _pointSpawner; 

    public float SecondsPerSpawn; 
    private float _timer;

    public AudioSource PositiveSound;
    public AudioSource NegativeSound;
    public AudioSource NeutralSound;
    private ScoreGUI _scoreHandler;

    private SceneFade _sceneFade; 

    private int _spawnSize = 1;
    private int _currentProgress = 0;
    private int _nextLevel = 25;
    private int _streak = 0;

    public bool Cheat = false; 

	void Start ()
	{
	    _spawner = GetComponent<Spawn>();
        _pointSpawner = GetComponent<SpawnPoints>();
	    _scoreHandler = GetComponent<ScoreGUI>();
	    _sceneFade = FindObjectOfType<SceneFade>();
        if (_sceneFade != null) 
            _sceneFade.FadeOut();
	    InputManager.Instance.Swipe += OnSwipe;
	}
	
	void Update ()
	{
        if (_spawner == null) return;
        _timer += Time.deltaTime;
	}

    public void NewGame()
    {
        _spawnSize = 1;
        _currentProgress = 0;
        _nextLevel = 25;
        _streak = 0; 

        _scoreHandler.NewGame();
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
        _currentProgress ++; 
    }

    public void OnSwipe(SwipeDirection dir)
    {
        if ((int)dir == (int)_spawner.LastSpawRotation || Cheat)
        {
            if (_timer < 0.8)
                _streak++;
            else _streak = 1;
            if (PositiveSound.pitch < 2)
                PositiveSound.pitch += 0.01f;
            PositiveSound.Play();

            if (_scoreHandler != null)
            {
                var score = _scoreHandler.CalucalteScore(_timer, _streak, _spawnSize);

                _pointSpawner.AddPoints(score, _spawner.LastPosition, _streak > 1);

                _scoreHandler.AddScore(score);
            }
            _timer = 0;
            _spawner.AnimeSpawn();
            SpawnNewArrows();
        }
        else
        {
            _streak = 0;
            NegativeSound.Play();
            _pointSpawner.AddPoints((int)-_scoreHandler.GetScore(), _spawner.LastPosition);

            _scoreHandler.AddScore((int)-_scoreHandler.GetScore());

            PositiveSound.pitch = 1f;
            NewGame();
            _timer = 0; 
        }
    }
}
