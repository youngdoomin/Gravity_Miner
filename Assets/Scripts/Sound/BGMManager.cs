using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] bgmClip;

    public int audioNumber;
    [SerializeField] private int introPool = 3;

    public static BGMManager instance = null;
    public static BGMManager Instance
    {
        get { return instance; }
    }


    private void Awake()
    {
        if(instance != null && instance != this)    // bgm매니저는 하나만 있어야함
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
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
        audioSource.Stop();
        audioNumber = num;
        audioSource.clip = bgmClip[num];
        audioSource.Play();

    }
    
}
