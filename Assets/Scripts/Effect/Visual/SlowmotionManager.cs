using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowmotionManager : MonoBehaviour
{

    public static bool isSlowTime;    //슬로우모션 변수
    public static float slowmotionScale = 0.2f; //슬로우모션 배율
    public static bool isPaused = false;
    private string stScene = "Flower_Rain";
    public GameObject[] settingPopup;
    private void Start()
    {
        if (stScene == SceneManager.GetActiveScene().name)
        { ClosePopup(); }

        isSlowTime = false;    //슬로우모션 변수
        slowmotionScale = 0.2f; //슬로우모션 배율
        isPaused = false;
    }
    void Update()
    {
        if (Playercontroller.life > 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // Esc 버튼 눌렀을 때
            {
                isPaused = !isPaused;   // 일시정지 온오프
                if (isPaused == true)    //일시정지가 밎다면
                { OpenPopup(0); } // 시간 멈춤
                else                    //아니면
                { PlayTime(); } // 시간 흐름
            }
            if (isPaused != true)
            { SlowMotion(); }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                goHome();
            }
            else if (Input.GetKeyDown("return"))
            {
                again();
            }
        }
    }
    public void goHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start_Screen", LoadSceneMode.Single);
    }

    public void again()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayTime()
    {
        ClosePopup();
        isPaused = false;
        Time.timeScale = 1;
    }

    public void OpenPopup(int i)
    {

        settingPopup[i].SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }
    private void ClosePopup()
    {
        foreach (GameObject uiPopup in settingPopup)
            uiPopup.SetActive(false);
    }
    public void SlowMotion()  //파워 사용중엔 시간이 느리게 흐름
    {
        if ((PGravity.screenFilter == true || Score.scoreActive == false) && (stScene == SceneManager.GetActiveScene().name)) // 슬로우모션 조건 : 에너지가 남아있다 && 파워 버튼을 눌렀다
        {
            isSlowTime = true;
            Time.timeScale = slowmotionScale;
        }
        else
        {
            isSlowTime = false;
            Time.timeScale = 1;
        }

        Time.timeScale = Mathf.Clamp(Time.timeScale, slowmotionScale, 1);
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // 이미지 끊기는거 방지
    }
}