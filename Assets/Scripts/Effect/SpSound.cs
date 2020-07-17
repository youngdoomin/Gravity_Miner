using UnityEngine;

public class SpSound : MonoBehaviour
{
    private AudioSource SpAudio;
    public AudioClip JamSound;
    private bool dontLoop;

    void Start()
    {
        SpAudio = gameObject.AddComponent<AudioSource>();
        SpAudio.clip = JamSound;
        SpAudio.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SubGravity.sp == SubGravity.speedLock)
        {
            if (dontLoop == false)
            {
                SpAudio.Play();
                dontLoop = true;
            }
        }
        else
        { dontLoop = false; }
    }
}
