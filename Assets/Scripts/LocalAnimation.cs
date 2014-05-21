using UnityEngine;

[RequireComponent(typeof(Animation))]

public class LocalAnimation : MonoBehaviour
{

    Vector3 _localPos;
    bool _wasPlaying;

    void Awake()
    {
        _localPos = transform.position;
        _wasPlaying = false;
    }

    void LateUpdate()
    {
        if (!animation.isPlaying && !_wasPlaying) return;
        transform.localPosition += _localPos;
        _wasPlaying = animation.isPlaying;
    }
}