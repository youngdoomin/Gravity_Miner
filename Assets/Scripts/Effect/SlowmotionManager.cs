using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowmotionManager : MonoBehaviour
{
    public const float slowmotionScale = 0.1f; //슬로우모션 배율
    private string stScene = "Flower_Rain";
    public GameObject[] settingPopup;

    public GameObject[] tutoUi;
    public GameObject[] button;

    public GameObject credit;
    public GameObject cdt_esc;

    const int Canv_IngamePause = 0;
    public const int Canv_Gameover = 1;
    const int Canv_Setting = 2;
    const int Canv_Tutorial = 3;
    const int Canv_Credit = 4;
    const int Canv_Shutdown = 5;
    private int page;
    bool isTuto;

    private void Start()
    {
        PlayTime();
        //DeactiveCredit();
        //DeactiveTutorial();
        //AccountInfoManager.account.info.hiScore = 0;
    }
    private void OnLevelWasLoaded(int level)
    {
        ClosePopupAll();
        if (AccountInfoManager.account.info.hiScore == 0)
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
        if (stScene == SceneManager.GetActiveScene().name) //게임중
        {
            if (GameManager.Instance.life > 0)
            {
                if (GameManager.Instance.isPaused != true)
                { SlowMotion(); }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            { goHome(); }
            else if (Input.GetKeyDown("return"))
            { again(); }
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) // 뒤로가기 누르면 종료문구
            OpenPopup(Canv_Shutdown);
        if (Input.GetKeyDown(KeyCode.Escape)) // Esc 누르면 일시정지 혹은 팝업창 닫기
            ClosePopupTop();
    }
    public void GamePause()
    {
        if (stScene == SceneManager.GetActiveScene().name) //게임중 여부에 따라 일시정지창이 다름
        {
            OpenPopup(Canv_IngamePause);
        }
        else
        {
            OpenPopup(Canv_Setting);
        }
    }
    public void goHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start_Screen", LoadSceneMode.Single);
        SoundManager.instance.PlayRandomIntro();
    }
    public void again()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ClosePopupAll();
    }
    public void PlayTime()
    {
        ClosePopupAll();
        DeactiveTutorial();
    }
    public void OpenPopup(int i)
    {
        settingPopup[i].SetActive(true);
        GameManager.Instance.isPaused = true;
        Time.timeScale = 0;
        if (i == Canv_Tutorial)
        {
            ActiveTutorial();
        }
    }
    public void ClosePopup(int i) //원하는 팝업창만 닫기
    {
        settingPopup[i].SetActive(false);
        if (i == Canv_Tutorial)
        {
            DeactiveTutorial();
        }
        bool allClosed = true;
        for (int j = 0; j < settingPopup.Length; j++)
        {
            if (settingPopup[j].activeSelf == true)
            {
                allClosed = false;
            }
        }
        if (allClosed == true)  //모든 팝업창이 꺼져있으면 게임 시작
        {
            GameManager.Instance.isPaused = false;
            Time.timeScale = 1;
        }
    }
    private void ClosePopupAll() //모든 팝업창 닫기
    {
        foreach (GameObject uiPopup in settingPopup)
            uiPopup.SetActive(false);
        GameManager.Instance.isPaused = false;
        Time.timeScale = 1;
    }
    public void ClosePopupTop() //가장 위의 팝업 하나 닫기
    {
        for (int i = settingPopup.Length - 1; i >= 0; i--)
        {
            if (settingPopup[i].activeSelf == true)
            {
                ClosePopup(i);
                break;
            }
            if (i == 0) //아무 팝업도 안열려있으면 일시정지
            {
                GamePause();
            }
        }
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
        isTuto = true;
        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(true);
        }
        ChangePage(0);
    }
    public void DeactiveTutorial()
    {
        isTuto = false;
        page = 0;
    }
    public void ChangePage(int i)
    {
        if ((i == -1 && page == 0) || (i == 1 && page == tutoUi.Length - 1)) { return; }
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
    public void IsActiveCredit(bool b)
    {
        credit.SetActive(b);
        cdt_esc.SetActive(b);
    }

}