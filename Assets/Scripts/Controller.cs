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

    private int _spawnSize = 1;
    private int _currentProgress = 0;
    private int _nextLevel = 50;

    private int _streak = 0;  

	void Start ()
	{
	    _spawner = GetComponent<Spawn>();
        _pointSpawner = GetComponent<SpawnPoints>();

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
            _nextLevel *= 2; 
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
            _pointSpawner.AddPoints(CalucalteScore(_timer, _streak, _spawnSize), _spawner.LastPosition);
            _timer = 0; 
        }
        else
        {
            _streak = 0;
            NegativeSound.Play();
            _pointSpawner.AddPoints(-50, _spawner.LastPosition);
            PositiveSound.pitch = 1f;
            _timer = 0; 
        }

        _spawner.AnimeSpawn();
        SpawnNewArrows();
    }

    public int CalucalteScore(float time, int streak, int cntArrows)
    {
        var timeBonus = 10000 / (_timer * 1000);
        if (timeBonus > 100 || timeBonus <= 0) timeBonus = 100; 
        var streakBonus = streak*cntArrows; 
        int total = (int)(timeBonus * streakBonus);
        Debug.Log("Timebonus: " + timeBonus + " StreakBonus: " + streakBonus + " Total =" + total );

        return total;
    }
}
