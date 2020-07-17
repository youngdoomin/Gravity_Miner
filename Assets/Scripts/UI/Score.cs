using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreUI;   // 텍스트
    public Text highScore; // 최고점수
    private Text finScore; // 게임 종료 점수
    public Text finHighScore;
    private string exhi = "HighScore";
    public static float scoreCt = 0;    // 점수 받는 변수
    public static bool scorecap = false;
    public static bool scoreActive = true;

    private int hiScoreCt = 0;
    // Start is called before the first frame update
    public GameObject endPopup;

    private string digit = "1";
    private string zero = "000000000000";

    void Start()
    {
        hiScoreCt = PlayerPrefs.GetInt(exhi, 0);
        highScore.text = hiScoreCt.ToString("0");
        scoreUI = GameObject.Find("ScoreVal").GetComponent<Text>();    // 스코어라는 text탐색

        scoreCt = 0;    // 점수 받는 변수
        scorecap = false;
        scoreActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCt >= hiScoreCt)
        {
            PlayerPrefs.SetInt(exhi, (int)scoreCt);
            highScore.text = scoreUI.text;
        }
        if (int.Parse(highScore.text) > int.Parse(digit))
        {
            digit = digit + "0";
            zero = zero.Substring(1);
        }


        if (Playercontroller.life > 0) //체력이 0 이하면 점수가 오르지 않음
        { scoreActive = true; }
        else
        {
            if(SlowmotionManager.isPaused == true)
            {
                scoreActive = false;
                finScore = GameObject.Find("FinalScore").GetComponent<Text>();
                finScore.text = scoreUI.text;

                finHighScore = GameObject.Find("HighScore_Val").GetComponent<Text>();
                finHighScore.text = zero + highScore.text;

            }
        }

        if (scorecap == false && scoreActive == true)
        {
            if (SubGravity.sp < SubGravity.speedLock)
            { scoreCt += SubGravity.sp * Time.deltaTime; }  // 시간 당 속도를 계속 더함
            else
            { scoreCt += SubGravity.sp * Time.deltaTime * 2; }  // 시간 당 속도를 계속 더함
            printScore();   // 점수 출력
        }
        if (Jam_ReturnSprite.getJam == true)
        {
            scoreCt += 100;
            Jam_ReturnSprite.getJam = false;
        }
        if (JamUI.collect == true)
        {
            scoreCt += 1000;
            JamUI.collect = false;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Reset();
        }
    }

    void printScore()
    {
        int intScore = (int)scoreCt;    // 정수형 변수로 바꿔서 받음(소수점 제거)
        string scoreStr = intScore.ToString();  // 정수를 문자열로
        scoreUI.text = scoreStr;    // 텍스트에다 그 문자열을 넣음
    }


    public void Reset()
    {
        PlayerPrefs.DeleteKey(exhi);
    }


}
