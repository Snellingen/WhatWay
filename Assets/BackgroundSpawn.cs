using UnityEngine;

public class BackgroundSpawn : MonoBehaviour
{

    public float SecondsPerSpawn;
    private float _timer;
    private Spawn _spawner;
    private bool _start; 

    // Use this for initialization
    void Start()
    {
        _spawner = GetComponent<Spawn>();
        _start = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawner == null) return;
        if (_timer >= SecondsPerSpawn)
        {
            Spawn();
            _timer = 0; 
        }

        _timer += Time.deltaTime;
    }

    public void Spawn()
    {
        if (_start)
        {
            _spawner.AnimeSpawn();
            _start = false; 
        }
        _spawner.SpawnArrows((SpawnType)Random.Range(0, 4), (SpawnRotation)Random.Range(0, 4), 1);
        _spawner.AnimeSpawn();
    }
}
