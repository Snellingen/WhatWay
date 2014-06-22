using System.Collections;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public GameObject TutorialText;
    private TutText _tutText;

    public AudioSource TutSound; 

    public GameObject Hand;
    public string[] AnimationQueueH;
    private int _handIndex = 0;

    public GameObject Arrow1;
    public string[] AnimationQueueA1;
    private int _a1Index = 0;

    public GameObject Arrow2;
    public string[] AnimationQueueA2;
    private int _a2Index = 0;

    public GameObject Arrow3;
    public string[] AnimationQueueA3;
    private int _a3Index = 0;

    public GameObject Circle;
    public string[] AnimationQueueCircle;
    private int _circleIndex = 0;

    public GameObject PlayGameButton;
    public int PlayButtonAppearIndex = 0; 


	void Start () {
        InputManager.Instance.Tap += OnTap;
	    _tutText = TutorialText.GetComponent<TutText>();


	}

    public void OnTap(Vector2 pos, TapState tapState)
    {
        if (tapState != TapState.Tapped) return;

        if (TutSound != null)
            TutSound.Play();

        _tutText.CurrentIndex++;
        _circleIndex++; 
        _handIndex ++;
        _a1Index ++;
        _a2Index ++;
        _a3Index ++;


        if (_handIndex < AnimationQueueH.Length)
        {
            Hand.animation.Play(AnimationQueueH[_handIndex]);
        }

        if (_a1Index < AnimationQueueA1.Length)
        {
            Arrow1.animation.Play(AnimationQueueA1[_a1Index]);
        }

        if (_a2Index < AnimationQueueA2.Length)
        {
            Arrow2.animation.Play(AnimationQueueA2[_a2Index]);
        }

        if (_a3Index < AnimationQueueA2.Length)
        {
            Arrow3.animation.Play(AnimationQueueA3[_a3Index]);
        }

        if (_circleIndex < AnimationQueueCircle.Length)
        {
            Circle.animation.Play(AnimationQueueCircle[_circleIndex]);
        }

        if (_tutText.CurrentIndex >= PlayButtonAppearIndex)
        {
            PlayGameButton.SetActive(true);
        }

    }


    void OnDestroy()
    {
        if ( InputManager.Instance != null )
            InputManager.Instance.Tap -= OnTap;
    }

}
