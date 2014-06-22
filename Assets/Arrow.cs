using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public void Recycle(int delay)
    {
        Invoke("_recycle", delay);
    }

    private void _recycle()
    {
        this.Recycle();
    }
	
}
