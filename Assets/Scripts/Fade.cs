using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{

    public float FadePerSeconds;
    private float _timer;
    private TextMesh _mesh; 

	// Use this for initialization
	void Start ()
	{
	    _mesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        _timer += Time.deltaTime; 
        _mesh.color = new Color(_mesh.color.r, _mesh.color.g, _mesh.color.b, _mesh.color.a - FadePerSeconds);
	    _timer -= FadePerSeconds; 

	}
}
