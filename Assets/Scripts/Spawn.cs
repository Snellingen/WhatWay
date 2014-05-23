using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SpawnType
{
    Arrow,
    Line,
    Diagonal,
    Circle
}

public enum SpawnRotation
{
    Right,
    Left,
    Up,
    Down
}

public class Spawn : MonoBehaviour
{
    public GameObject SpawnObject;
    public int ZIndex = 0;
    public float Offset = 0.64f;
    public float ScreenOffsetTop = 0.7f;
    public float ScreenOffsetBottom = 0.7f;
    public float ScreenOffsetLeft = 0.5f;
    public float ScreenOffsetRight = 0.5f;

    [HideInInspector]
    public SpawnRotation LastSpawRotation;
    public Vector2 LastPosition; 
    public List<Object> LastSpawned = new List<Object>();

    private GameObject _rightSpawn;
    private Vector2 _screenSize;
    

    void Start()
    {
        _screenSize = new Vector2(Screen.width, Screen.height);
        StartGame();
    }

    public void StartGame()
    {
        var spwnpos = Camera.main.ScreenToWorldPoint(new Vector2(_screenSize.x / 2, _screenSize.y / 2));
        spwnpos.z = 10; 
        UpdateSpawnArea();
        SpawnArrow(SpawnRotation.Right, 1, spwnpos);
    }

    public void AnimeSpawn()
    {
        if (_rightSpawn != null)
        {
            switch (LastSpawRotation)
            {
                case SpawnRotation.Left:
                    _rightSpawn.animation.Play("MoveLeft");
                    break;
                case SpawnRotation.Right:
                    _rightSpawn.animation.Play("MoveRight");
                    break;
                case SpawnRotation.Down:
                    _rightSpawn.animation.Play("MoveDown");
                    break;
                case SpawnRotation.Up:
                    _rightSpawn.animation.Play("MoveUp");
                    break;
            }
        }

        Destroy(_rightSpawn, 2);

        if (LastSpawned.Count == 0) return;
        foreach (var go in LastSpawned.Select(o => o as GameObject))
        {
            go.animation.Play("FadeFast");
            Destroy(go, 1);
        }
        LastSpawned.Clear();
    }

    public void UpdateSpawnArea()
    {
        _screenSize = new Vector2(Screen.width, Screen.height);
    }
    public bool SpawnArrows(SpawnType type, SpawnRotation rotation, int spawnSize = 5)
    {
        UpdateSpawnArea();
        LastSpawRotation = rotation;

        Vector2 screenOffset = Camera.main.WorldToScreenPoint(new Vector3(Offset, Offset));

        var sOffXl = screenOffset.x * ScreenOffsetLeft;
        var sOffXr = screenOffset.x * ScreenOffsetRight;
        var sOffYt = screenOffset.x * ScreenOffsetTop;
        var sOffYb = screenOffset.x * ScreenOffsetBottom;

        var spwnpos = Camera.main.ScreenToWorldPoint(new Vector3(
            Random.Range(sOffXl, _screenSize.x - sOffXr),
            Random.Range(sOffYt, _screenSize.y - sOffYb)
            ));
        spwnpos.z = ZIndex;
        LastPosition = spwnpos;

        switch (type)
        {
            case SpawnType.Arrow:
                SpawnArrow(rotation, spawnSize, spwnpos);
                break;
            case SpawnType.Diagonal:
                SpawnDiagonal(rotation, spawnSize, spwnpos);
                break;
            case SpawnType.Line:
                SpawnLine(rotation, spawnSize, spwnpos);
                break;
            case SpawnType.Circle:
                SpawnCircle(rotation, spawnSize, spwnpos);
                break;
        }
        return true;
    }

    private void SpawnArrow(SpawnRotation rotation, int spawnSize, Vector3 spwnpos)
    {
        var flockRotation = (SpawnRotation)Random.Range(0, 4);
        var spawnRotation = Rotate(transform.rotation, rotation);
        var defaultRotation = Rotate(transform.rotation, (SpawnRotation)Random.Range(0, 4));

        switch (flockRotation)
        {
            case SpawnRotation.Right:
                spwnpos.x -= Offset / 2 * spawnSize / 4;
                spwnpos.y += (Offset * spawnSize / 2.3f);
                break;
            case SpawnRotation.Left:
                spwnpos.x += Offset / 2 * spawnSize / 4;
                spwnpos.y += (Offset * spawnSize / 2.3f);
                break;
            case SpawnRotation.Down:
                spwnpos.x += (Offset * spawnSize / 2.3f);
                spwnpos.y += Offset / 2 * spawnSize / 4;
                break;
            case SpawnRotation.Up:
                spwnpos.x += (Offset * spawnSize / 2.3f);
                spwnpos.y -= Offset / 2 * spawnSize / 4;
                break;
        }

        var randomInt = Random.Range(0, spawnSize - 1);
        for (var i = 0; i < spawnSize; i++)
        {
            if (randomInt == i)
                _rightSpawn = Instantiate(SpawnObject, spwnpos, spawnRotation) as GameObject;
            else LastSpawned.Add(Instantiate(SpawnObject, spwnpos, defaultRotation));

            switch (flockRotation)
            {
                case SpawnRotation.Right:
                    spwnpos.x += i < (spawnSize / 2) ? Offset : -Offset;
                    spwnpos.y -= Offset;
                    break;
                case SpawnRotation.Left:
                    spwnpos.x += i < (spawnSize / 2) ? -Offset : Offset;
                    spwnpos.y -= Offset;
                    break;
                case SpawnRotation.Down:
                    spwnpos.y += i < (spawnSize / 2) ? -Offset : Offset;
                    spwnpos.x -= Offset;
                    break;
                case SpawnRotation.Up:
                    spwnpos.y += i < (spawnSize / 2) ? Offset : -Offset;
                    spwnpos.x -= Offset;
                    break;
            }
        }
    }
    private void SpawnCircle(SpawnRotation rotation, int spawnSize, Vector3 spwnpos)
    {
        var spawnRotation = Rotate(transform.rotation, rotation);
        var defaultRotation = Rotate(transform.rotation, (SpawnRotation)Random.Range(0, 4));

        var randomInt = Random.Range(0, spawnSize - 1);

        for (var i = 0; i < spawnSize; i++)
        {
            var radius = (spawnSize * Offset) / Mathf.PI;
            var angle = (360 / spawnSize) * i;

            var pos = new Vector2(
                (float)(spwnpos.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad)),
                (float)(spwnpos.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad))
                );

            if (randomInt == i)
                _rightSpawn = Instantiate(SpawnObject, pos, spawnRotation) as GameObject;
            else LastSpawned.Add(Instantiate(SpawnObject, pos, defaultRotation));
        }
    }
    private void SpawnDiagonal(SpawnRotation rotation, int spawnSize, Vector3 spwnpos)
    {
        var flockRotation = (SpawnRotation)Random.Range(0, 4);
        var spawnRotation = Rotate(transform.rotation, rotation);
        var defaultRotation = Rotate(transform.rotation, (SpawnRotation)Random.Range(0, 4));

        spwnpos.x += flockRotation == SpawnRotation.Left || flockRotation == SpawnRotation.Down
                    ? -(Offset * spawnSize / 2.3f)
                    : Offset * spawnSize / 2.3f;
        spwnpos.y += (Offset * spawnSize / 2.3f);

        var randomInt = Random.Range(0, spawnSize - 1);
        for (var i = 0; i < spawnSize; i++)
        {
            if (randomInt == i)
                _rightSpawn = Instantiate(SpawnObject, spwnpos, spawnRotation) as GameObject;
            else LastSpawned.Add(Instantiate(SpawnObject, spwnpos, defaultRotation));

            spwnpos.x += flockRotation == SpawnRotation.Left || flockRotation == SpawnRotation.Down ? Offset : -Offset;
            spwnpos.y -= Offset;
        }
    }
    private void SpawnLine(SpawnRotation rotation, int spawnSize, Vector3 spwnpos)
    {
        var flockRotation = (SpawnRotation)Random.Range(0, 4);
        var spawnRotation = Rotate(transform.rotation, rotation);
        var defaultRotation = Rotate(transform.rotation, (SpawnRotation)Random.Range(0, 4));

        if (flockRotation == SpawnRotation.Left || flockRotation == SpawnRotation.Right)
            spwnpos.x -= (Offset * spawnSize / 2.3f);
        else spwnpos.y += (Offset * spawnSize / 2.3f);
        var randomInt = Random.Range(0, spawnSize - 1);
        for (var i = 0; i < spawnSize; i++)
        {
            if (randomInt == i)
                _rightSpawn = Instantiate(SpawnObject, spwnpos, spawnRotation) as GameObject;
            else LastSpawned.Add(Instantiate(SpawnObject, spwnpos, defaultRotation));

            if (flockRotation == SpawnRotation.Left || flockRotation == SpawnRotation.Right)
                spwnpos.x += Offset;
            else spwnpos.y -= Offset;
        }
    }

    private static Quaternion Rotate(Quaternion q, SpawnRotation rotation)
    {
        switch (rotation)
        {
            case SpawnRotation.Right:
                q *= Quaternion.Euler(0, 0, 0);
                break;
            case SpawnRotation.Left:
                q *= Quaternion.Euler(0, 0, 180);
                break;
            case SpawnRotation.Up:
                q *= Quaternion.Euler(0, 0, 90);
                break;
            case SpawnRotation.Down:
                q *= Quaternion.Euler(0, 0, -90);
                break;
            default:
                q *= Quaternion.Euler(0, 0, 0);
                break;
        }
        return q;
    }
}
