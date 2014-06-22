using UnityEngine;

public class ChangeScene : ButtonTask {

    private SceneFade _sceneFade;
    public string ChangeTo = "GameScene";

    public bool UseIndex = false;
    public int Index;

    private bool Activated = false; 

    void Start()
    {
        _sceneFade = FindObjectOfType<SceneFade>();
        _sceneFade.FadeInDone += SwitchScene;
    }

    public override void Activate()
    {
        Activated = true; 
        _sceneFade.FadeIn();
    }

    public void SwitchScene()
    {
        if (!Activated) return;
        Activated = false; 
        Application.LoadLevel(ChangeTo);
        _sceneFade.FadeOut();
    }

    void OnDestroy()
    {
        if (_sceneFade != null)
        {
            _sceneFade.FadeInDone -= SwitchScene; 
        }
    }
}
