using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    public AudioMixer masterMixer;

    public static AudioMixerManager instance = null;
    public static AudioMixerManager Instance
    {
        get { return instance; }
    }

    public float lowpassOn = 500;
    public float lowpassOff = 10000;

    private void Awake()
    {

        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetBgmLv(float bgmLv)
    {
        masterMixer.SetFloat("BgmVol", Mathf.Log10(bgmLv) * 20);
    }
    public void SetSfxLv(float sfxLv)
    {
        masterMixer.SetFloat("SfxVol", Mathf.Log10(sfxLv) * 20);
    }

    public void Setlowpass(float value)
    {
        masterMixer.SetFloat("BgmLowpass", value);
        
    }

}
