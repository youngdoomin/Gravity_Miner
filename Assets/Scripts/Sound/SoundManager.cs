using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    private AudioSource bgmSource;
    private AudioSource effectSource;
    public AudioClip fullSpeed;
    public AudioClip blockEf;
    public AudioClip damage;
    public AudioClip kill;
    public AudioClip lootItem;
    public AudioMixerGroup bgmOutput;

    [SerializeField] private AudioClip[] bgmClip;

    public int audioNumber;
    [SerializeField] private int introPool = 3;

    public static SoundManager instance = null;
    public static SoundManager Instance
    {
        get { return instance; }
    }


    private void Awake()
    {
        AccountInfoManager.account.info.SaveMixerSetting(bgmOutput);
        if (instance != null && instance != this)    // bgm매니저는 하나만 있어야함
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);


        GameObject child = new GameObject("BGM");
        child.transform.SetParent(transform);
        bgmSource = child.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = bgmOutput;

        effectSource = GetComponent<AudioSource>();
        effectSource.loop = false;
    }
    void Start()
    {
        PlayRandomIntro();
    }

    public void PlayRandomIntro() //무작위 인트로음악 재생
    {
        int temp = Random.Range(0, introPool);
        ChangeClip(temp);
    }

    public void PlayIngameLoop() // 인게임음악 재생
    {
        if (audioNumber < introPool)
        {
            ChangeClip(audioNumber + introPool);
        }
        else
        {
            Debug.Log("현재 인트로음악이 아님");
        }
    }


     public void ChangeClip(int num)
    {
        bgmSource.Stop();
        audioNumber = num;
        bgmSource.clip = bgmClip[num];
        bgmSource.Play();

    }

    public void PlaySE(AudioClip clip)
    {
        effectSource.Stop();
        effectSource.clip = clip;
        effectSource.Play();
    }
    
}
