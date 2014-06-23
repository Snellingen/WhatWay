using System;
using System.Threading;
using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public float StartTimeSeconds;
    private TextMesh _output;
    private bool _hasOutput = false;
    private bool _start = false;
    private bool _pause; 
    [HideInInspector] public float Timer;
    public bool TimeOut = false; 

    void Start()
    {
        _output = GetComponent<TextMesh>();
        _hasOutput = _output != null;
    }

    public void StartClock()
    {
        _start = true; 
    }

    public void StopClock()
    {
        _start = false; 
    }

    public void ResetClock()
    {
        Timer = 0;
        _start = false;
        TimeOut = false; 
    }

    public void SetPause(bool value)
    {
        _pause = value; 
    }

    void Update ()
    {

        if (!_start) return; 
        if (_pause) return;
        Timer += Time.deltaTime;
        if (Timer >= StartTimeSeconds)
        {
            TimeOut = true;
            Timer = StartTimeSeconds; 
            SetPause(true); 
        }
        if (!_hasOutput)return;

        var span = TimeSpan.FromSeconds(StartTimeSeconds - Timer);
        _output.text = string.Format("{0:00}:{1:00}", (int)span.TotalMinutes, span.Seconds);

    }
}
