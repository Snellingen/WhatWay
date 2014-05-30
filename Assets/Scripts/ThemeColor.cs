using UnityEngine;

public enum ColorTheme
{
    Day, Night
}

public enum TypeColor
{
    Sprite,
    GUI,
    Camera,
    TextMesh,
    GUIAlpha
}

public class ThemeColor : MonoBehaviour
{
    public ColorTheme ActiveTheme = ColorTheme.Day;
    public TypeColor Type = TypeColor.Sprite;
    public Color[] Colors;
    public bool Instatiated; 

    void Awake()
    {
        GameData.Instance.ThemeChange += SwitchTheme;
        ActiveTheme = GameData.Instance.CurrenTheme;
        SwitchTheme(ActiveTheme);
    }


    void SwitchTheme(ColorTheme newTheme)
    {
        ActiveTheme = newTheme;
        switch (Type)
        {
            case TypeColor.Sprite:
                GetComponent<SpriteRenderer>().color = Colors[(int)ActiveTheme];
                break;
            case TypeColor.GUI:
                GetComponent<GUITexture>().color = Colors[(int)ActiveTheme];
                break;
            case TypeColor.Camera:
                GetComponent<Camera>().backgroundColor = Colors[(int)ActiveTheme];
                break;
            case TypeColor.TextMesh:
                GetComponent<TextMesh>().color = Colors[(int) ActiveTheme];
                break; 
            case TypeColor.GUIAlpha:
                var guiTex = GetComponent<GUITexture>();
                var newColor = Colors[(int)ActiveTheme];
                newColor.a = guiTex.color.a;
                GetComponent<GUITexture>().color = newColor;
                break;
        }
    }

    void OnDestroy()
    {
        if (GameData.Instance != null)
            GameData.Instance.ThemeChange -= SwitchTheme;
    }
}
