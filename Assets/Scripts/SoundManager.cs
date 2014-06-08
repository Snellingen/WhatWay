using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    private AudioListener listener; 

	// Use this for initialization
	void Start ()
	{

	    listener = GetComponent<AudioListener>();
        if (listener == null) return;
	    if (GameData.Instance.Mute)
            listener.audio.enabled = false; 

	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (listener == null) return; 
        if (GameData.Instance.Mute)
            listener.enabled = false; 
        else if (!listener.enabled)
            listener.enabled = true; 
    }
}
