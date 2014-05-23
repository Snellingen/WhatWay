using UnityEngine;

[RequireComponent(typeof(Spawn))]
[RequireComponent(typeof(SpawnPoints))]
public class Controller : MonoBehaviour
{

    public GameObject SpawObject;
    private Spawn _spawner;
    private SpawnPoints _pointSpawner; 

    public float SecondsPerSpawn; 
    private float _timer;

    public AudioSource PositiveSound;
    public AudioSource NegativeSound;
    public AudioSource NeutralSound;

    private ScoreGUI _scoreHandler; 

    private int _spawnSize = 1;
    private int _currentProgress = 0;
    private int _nextLevel = 25;
    private int _streak = 0;

	void Start ()
	{
	    _spawner = GetComponent<Spawn>();
        _pointSpawner = GetComponent<SpawnPoints>();
	    _scoreHandler = GetComponent<ScoreGUI>();

	    InputManager.Instance.OnSwipe += OnSwipe;
	}
	
	void Update ()
	{
        if (_spawner == null) return;
        _timer += Time.deltaTime;
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
        if ((int)dir == (int)_spawner.LastSpawRotation)
        {
            _streak++;
            if (PositiveSound.pitch < 2)
                PositiveSound.pitch += 0.01f;
            PositiveSound.Play();

            if (_scoreHandler != null)
            {
                int score = _scoreHandler.CalucalteScore(_timer, _streak, _spawnSize);

                _pointSpawner.AddPoints(score, _spawner.LastPosition);

                _scoreHandler.AddScore(score);
            }
            _timer = 0; 
        }
        else
        {
            _streak = 0;
            NegativeSound.Play();
            _pointSpawner.AddPoints(-50, _spawner.LastPosition);

            _scoreHandler.AddScore(-50);

            PositiveSound.pitch = 1f;
            _timer = 0; 
        }

        _spawner.AnimeSpawn();
        SpawnNewArrows();
    }
}
