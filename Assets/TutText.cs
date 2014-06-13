using UnityEngine;
using System.Collections;

public class TutText : MonoBehaviour
{

    public string[] Text;
    public int CurrentIndex = 0;

    private TextMesh _txt;



    void Start()
    {
        _txt = GetComponent<TextMesh>();
        InputManager.Instance.Tap += OnTap; 
    }

	void Update ()
	{
	    _txt.text = CurrentIndex < Text.Length ? Text[CurrentIndex] : ""; 
	}

    public void OnTap(Vector2 pos, TapState tapState)
    {
        if (tapState == TapState.Tapped)
            CurrentIndex ++; 
    }

    void OnDestroy()
    {
        
    }
}
