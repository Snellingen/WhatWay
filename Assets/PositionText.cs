using UnityEngine;

public class PositionText : MonoBehaviour
{

    public Vector2 Position;

	// Use this for initialization
	void Start ()
	{
        UpdateTextAlignment();
	}

    void Update()
    {
        UpdateTextAlignment();
    }

    public void UpdateTextAlignment()
    {
        var pos = Camera.main.ScreenToWorldPoint(new Vector2(
            Screen.width * Position.x,
            Screen.height * Position.y
            ));

        pos.z = 100;
        transform.position = pos;
    }
}
