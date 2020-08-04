using UnityEngine;

public class SpSound : MonoBehaviour
{
    private bool dontLoop;

    // Update is called once per frame
    void Update()
    {
        if (SubGravity.sp == SubGravity.speedLock)
        {
            if (dontLoop == false)
            {
                SoundManager.instance.PlaySE(SoundManager.instance.fullSpeed);
                dontLoop = true;
            }
        }
        else
        { dontLoop = false; }
    }
}
