using UnityEngine;
using System.Collections;

public class TitleAdjust : MonoBehaviour
{

    public float Tolerance = 0.8f;
    public float Scale = 0.1f;

    private Vector3 _originial;

    void Start()
    {
        _originial = transform.localScale;
    }

    void Update()
    {
        AdjustWidth();
    }
    public void AdjustWidth()
    {
        float ratio = (float)Screen.width/Screen.height; 
        var scale = (ratio >= Tolerance) ? 1 : Scale; 

        transform.localScale = new Vector3(
            _originial.x * scale,
            _originial.y * scale,
            _originial.z * 1
            );
    }
}
