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
    }

	void Update ()
	{
	    _txt.text = CurrentIndex < Text.Length ? Text[CurrentIndex] : ""; 
	}

    void OnDestroy()
    {
        
    }
}
