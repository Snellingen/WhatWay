using UnityEngine;
using System.Collections;

public class ChangeScene : ButtonTask {

    private SceneFade _sceneFade;

    void Start()
    {
        _sceneFade = FindObjectOfType<SceneFade>();
        _sceneFade.FadeInDone += SwitchScene; 
    }

    public override void Activate()
    {
        _sceneFade.FadeIn();
    }

    public void SwitchScene()
    {
        Application.LoadLevel("GameScene");
        _sceneFade.FadeOut();
    }
}
