using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreUI;   // 텍스트
    public Text highScore; // 최고점수
    private Text finScore; // 게임 종료 점수
    private Text finHighScore;
    [SerializeField]
    private string exhi = "HighScore";
    public GameObject crown;
    private float waitTime;
    private float maxTime = 5;
        
    bool breakScore;

    [SerializeField]
    private int hiScoreCt = 0;
    public GameObject endPopup;

    private string digit = "1";
    private string zero = "000000000000";

    void Start()
    {

        hiScoreCt = AccountInfoManager.account.info.hiScore;
        //highScore.text = hiScoreCt.ToString("00000");
        scoreUI = GameObject.Find("ScoreVal").GetComponent<Text>();    // 스코어라는 text탐색

        hiScoreCt = 0;
        
    }

    void Update()
    {
        if ((int)GameManager.Instance.scoreCt > hiScoreCt)
        {
            hiScoreCt = (int)GameManager.Instance.scoreCt;
            AccountInfoManager.account.info.NewHiScore((int)GameManager.Instance.scoreCt);
            PlayerPrefs.SetInt(exhi, (int)GameManager.Instance.scoreCt);
            
            if (!breakScore) { breakScore = true; }
        }

        if (breakScore && waitTime < maxTime) { 
            waitTime += Time.deltaTime;
            Debug.Log(waitTime);
            Color c = crown.GetComponent<Image>().color;
            c.a = .3f + Mathf.PingPong(Time.time * 7f, .7f);
            crown.GetComponent<Image>().color = c;
        }
        else if (breakScore) { breakScore = false;
            Color c = crown.GetComponent<Image>().color;
            c.a = 1;
            crown.GetComponent<Image>().color = c;
        }

        if (int.Parse(highScore.text) > int.Parse(digit))
        {
            digit = digit + "0";
            zero = zero.Substring(1);
        }


        if (GameManager.Instance.life > 0) //체력이 0 이하면 점수가 오르지 않음
        { GameManager.Instance.scoreActive = true; }
        else
        {
            if(GameManager.Instance.isPaused == true && Time.timeScale == 0)
            {
                GameManager.Instance.scoreActive = false;
                finScore = GameObject.Find("FinalScore").GetComponent<Text>();
                finScore.text = scoreUI.text;

                finHighScore = GameObject.Find("HighScore_Val").GetComponent<Text>();
                finHighScore.text = zero + highScore.text;

            }
        }

        if (GameManager.Instance.scorecap == false && GameManager.Instance.scoreActive == true)
        {
            if (GameManager.Instance.sp < GameManager.Instance.speedLock)
            { GameManager.Instance.scoreCt += GameManager.Instance.sp * Time.deltaTime; }  // 시간 당 속도를 계속 더함
            else
            { GameManager.Instance.scoreCt += GameManager.Instance.sp * Time.deltaTime * 2; }  // 시간 당 속도를 계속 더함
            printScore();   // 점수 출력
        }
        if (GameManager.Instance.getJam == true)
        {
            GameManager.Instance.scoreCt += 100;
            GameManager.Instance.getJam = false;
        }
        if (GameManager.Instance.collect == true)
        {
            GameManager.Instance.scoreCt += 1000;
            GameManager.Instance.collect = false;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Reset();
        }
    }
    /*
    IEnumerator newScore(float aValue, float aTime)
    {
        breakScore = true;
        float alpha = crown.GetComponent<Image>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            crown.GetComponent<Image>().material.color = newColor;
            yield return null;
        }
    }
    */

    void printScore()
    {
        int intScore = (int)GameManager.Instance.scoreCt;    // 정수형 변수로 바꿔서 받음(소수점 제거)
        string scoreStr = intScore.ToString();  // 정수를 문자열로
        scoreUI.text = scoreStr;    // 텍스트에다 그 문자열을 넣음
    }


    public void Reset()
    {
        PlayerPrefs.DeleteKey(exhi);
    }

    
}
