using UnityEngine;

[RequireComponent(typeof(Animation))]

public class LocalAnimation : MonoBehaviour
{

    [HideInInspector]
    public Vector3 LocalPos;
    public bool WasPlaying;
    public bool Deactiveate; 


    void Awake()
    {
       Reset();
    }

    public void Reset()
    {
        LocalPos = transform.position;
        WasPlaying = false;
        Deactiveate = false; 
    }

    void LateUpdate()
    {
        if (Deactiveate) return; 
        if (!animation.isPlaying && !WasPlaying) return;
        transform.localPosition += LocalPos;
        WasPlaying = animation.isPlaying;
    }
}