using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{

    public LocalAnimation LocAni;


    public void Recycle(int delay)
    {
        Invoke("_recycle", delay);
    }

    private void _recycle()
    {

        LocAni.Deactiveate = true; 
        this.Recycle();
    }

    void OnEnable()
    {
        LocAni.Deactiveate = false; 
        LocAni.Reset();
    }
	
}
