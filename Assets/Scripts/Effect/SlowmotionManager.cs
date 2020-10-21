using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowmotionManager : MonoBehaviour
{
    public const float slowmotionScale = 0.2f; //슬로우모션 배율
    private string stScene = "Flower_Rain";
    public GameObject[] settingPopup;

    public GameObject[] tutoUi;
    public GameObject[] button;

    public GameObject credit;
    public GameObject cdt_esc;

    private int page;
    bool isTuto;
    private void Start()
    {
        //DeactiveCredit();
        DeactiveTutorial();

        //AccountInfoManager.account.info.hiScore = 0;
    }

    private void OnLevelWasLoaded(int level)
    {
        ClosePopup();
        if(AccountInfoManager.account.info.hiScore == 0)
        {
            ActiveTutorial();
            GameManager.Instance.isPaused = true;
        }
    }


    void Update()
    {
        if (isTuto)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { ChangePage(-1); }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) { ChangePage(1); };

        }

        if (stScene == SceneManager.GetActiveScene().name)
        {
            if (GameManager.Instance.life > 0)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) // Esc 버튼 눌렀을 때
                { TimeSwitch(0); }
                else if (Input.GetKeyDown(KeyCode.Backspace)) { TimeSwitch(1); }
                if (GameManager.Instance.isPaused != true)
                { SlowMotion(); }

            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            { goHome(); }
            else if (Input.GetKeyDown("return"))
            { again(); }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // Esc 버튼 눌렀을 때
            { TimeSwitch(0); }
            else if (Input.GetKeyDown(KeyCode.Backspace)) { TimeSwitch(1); }

        }
    }

    void TimeSwitch(int i)
    {
        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;   // 일시정지 온오프
        if (GameManager.Instance.isPaused == true)    //일시정지가 밎다면
        { OpenPopup(i); } // 시간 멈춤
        else                    //아니면
        { PlayTime(); } // 시간 흐름
    }

    public void goHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start_Screen", LoadSceneMode.Single);
        SoundManager.instance.PlayRandomIntro();
        ClosePopup();
    }

    public void again()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ClosePopup();
    }

    public void PlayTime()
    {
        ClosePopup();
        DeactiveTutorial();
        GameManager.Instance.isPaused = false;
        Time.timeScale = 1;
    }

    public void OpenPopup(int i)
    {
        settingPopup[i].SetActive(true);
        GameManager.Instance.isPaused = true;
        Time.timeScale = 0;
    }

    private void ClosePopup()
    {
        foreach (GameObject uiPopup in settingPopup)
            uiPopup.SetActive(false);
    }
    void SlowMotion()  //파워 사용중엔 시간이 느리게 흐름
    {
        //Debug.Log("slowmotion_Active");
        if (GameManager.Instance.screenFilter == true || GameManager.Instance.scoreActive == false) //&& (stScene == SceneManager.GetActiveScene().name)) // 슬로우모션 조건 : 에너지가 남아있다 && 파워 버튼을 눌렀다
        { Time.timeScale = slowmotionScale; }
        else
        { Time.timeScale = 1; }

        Time.timeScale = Mathf.Clamp(Time.timeScale, slowmotionScale, 1);
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // 이미지 끊기는거 방지
    }

    public void ActiveTutorial()
    {
        Time.timeScale = 0;
        isTuto = true;

        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(true);
        }

        ClosePopup();
        ChangePage(0);
    }
    public void DeactiveTutorial()
    {
        for (int i = 0; i < tutoUi.Length; i++)
        {
            tutoUi[i].SetActive(false);
        }

        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(false);
        }

        isTuto = false;
        page = 0;
        GameManager.Instance.isPaused = false;
        Time.timeScale = 1;
    }

    public void ChangePage(int i)
    {
        if((i == -1 && page == 0) || (i == 1 && page == tutoUi.Length - 1)) { return; }
        for (int idx = 0; idx < tutoUi.Length; idx++)
        {
            tutoUi[idx].SetActive(false);
        }
        if (page < tutoUi.Length)
        {
            page += i;
            tutoUi[page].SetActive(true);

            if (page == tutoUi.Length - 1)
            {
                button[1].SetActive(false);
            }
            else if (page == 0)
            {
                button[0].SetActive(false);
            }
            else
            {
                button[0].SetActive(true);
                button[1].SetActive(true);
            }


        }
        else { DeactiveTutorial(); }
    }

    public void DeactiveCredit()
    {
        credit.SetActive(false);
        cdt_esc.SetActive(false);
    }

    public void ActiveCredit()
    {
        credit.SetActive(true);
        cdt_esc.SetActive(true);
    }

}