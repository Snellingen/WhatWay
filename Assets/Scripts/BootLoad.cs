using UnityEngine;
using System.Collections;

public class BootLoad : MonoBehaviour
{
    public string LoadScene = "Menu"; 
	void Start () {
	    Application.LoadLevel(LoadScene);
	}

}
