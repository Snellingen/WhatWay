using System.Threading;
using UnityEngine;
using System.Collections;

public class ShowStatMenu : ButtonTask
{

    public GameObject BottomObject;
    public GameObject TopObject;

    public string AnimationNameBottom;
    public string AnimationNameTop; 

    public bool Reverse;

    public override void Activate()
    {
        if (Reverse)
        {
            TopObject.animation[AnimationNameTop].speed = -1;
            TopObject.animation[AnimationNameTop].time = TopObject.animation[AnimationNameTop].length;
            TopObject.animation.Play(AnimationNameTop);

            BottomObject.animation[AnimationNameBottom].speed = -1;
            BottomObject.animation[AnimationNameBottom].time = BottomObject.animation[AnimationNameBottom].length;
            BottomObject.animation.Play(AnimationNameBottom);

        }
        else
        {

            TopObject.animation[AnimationNameTop].speed = 1;
            TopObject.animation[AnimationNameTop].time = 0;
            TopObject.animation.Play(AnimationNameTop);

            BottomObject.animation[AnimationNameBottom].speed = 1;
            BottomObject.animation[AnimationNameBottom].time = 0;
            BottomObject.animation.Play(AnimationNameBottom);

            BottomObject.SetActive(true);
            if (BottomObject != null)
            {
                BottomObject.animation.Play(AnimationNameBottom);
            }
            if (TopObject != null)
            {
                TopObject.animation.Play(AnimationNameTop);
            }
        }
    }

}
