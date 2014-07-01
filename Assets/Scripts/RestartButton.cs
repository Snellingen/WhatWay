using UnityEngine;
using System.Collections;

public class RestartButton : ButtonTask
{

    public Controller GameController;
    public GameObject GameOverScreen;

    private Animation _launchAnimation;

    public float TimeToWaitBeforeDisable;
    private float _timer;
    private bool _alreadyActive = false; 
    private bool _startTimer = false;

    void Start()
    {
        _launchAnimation = GameOverScreen.GetComponent<Animation>();
    }

    public override void Activate()
    {
        if (_alreadyActive) return;
        _alreadyActive = true; 
        if (_launchAnimation == null) return;
        _launchAnimation["GameOver"].speed = -1;
        _launchAnimation["GameOver"].time =_launchAnimation["GameOver"].length;
        _launchAnimation.Play("GameOver");
        _startTimer = true; 
    }

    void Update()
    {
        if (!_startTimer) return; 
        _timer += Time.deltaTime;
        if (!(_timer >= TimeToWaitBeforeDisable)) return;

        if (_launchAnimation != null)
            _launchAnimation["GameOver"].speed = 1;
        GameController.NewGame();
        GameController.SetPause(false);
        _startTimer = false;
        _timer = 0;
        GameOverScreen.SetActive(false);
        _alreadyActive = false;
    }
}
