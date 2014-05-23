using UnityEngine;
using System.Collections;

public class TextAdjust : MonoBehaviour
{
    public int SortingLayer;

    private void Awake()
    {
        renderer.sortingOrder = SortingLayer;
    }
}
