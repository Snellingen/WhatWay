using UnityEngine;

public class BackButtonScript : ButtonTask {

    private SceneFade _sceneFade;
    public string ChangeTo = "GameScene";



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
