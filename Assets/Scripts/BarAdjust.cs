using UnityEngine;

public class BarAdjust : MonoBehaviour
{
    public GameObject LSide;
    public GameObject Middle;
    public GameObject RSide;

    public bool Top = false;
    public float PosY = 1.015f;
    public float PosX = 1.015f; 

    void Start()
    {
        AdjustWidth();
    }

    public void AdjustWidth()
    {
        Vector2 toScale = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Middle.transform.localScale = new Vector3((toScale.x * 4f), 1);
        Middle.transform.position = new Vector2(0, !Top ? -toScale.y * 1.015f : +toScale.y * 1.015f);
        LSide.transform.position = new Vector2(!Top ? -toScale.x * 0.9f : +toScale.x * 0.9f, !Top ? -toScale.y * 1.015f : +toScale.y * 1.015f);
        RSide.transform.position = new Vector2(!Top ? +toScale.x * 0.9f : -toScale.x * 0.9f, !Top ? -toScale.y * 1.015f : +toScale.y * 1.015f);
    }

    void Update()
    {
        AdjustWidth();
    }
}
