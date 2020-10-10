using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    public static float i = 0;
    Text comboUI;
    public const int timer = 20;
    public static int comboCt = 0;
    bool comboEnd = false;

    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        comboUI = GameObject.Find("Combo").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (comboCt > 0)    // 콤보가 일정 수 보다 크면
        {
            /*if(Enemy.kill == true)
            {                 
                lerpColor = Color.Lerp(Color.red, Color.white, 1);
                comboUI.color = lerpColor;
                
            }*/
            printCombo();
            if (i >= timer)
            {
                cbCC();
            }
            else if (i < timer)
            {
                i += Time.unscaledDeltaTime;

            }

        }
        else
        {
            nprintCombo();
        }

    }

    private void FixedUpdate()
    {
        if (comboEnd == true)
            GameObject.Find("ComboBar").SendMessage("BarOff");
    }

    void printCombo()
    {
        comboEnd = false;
        comboUI.CrossFadeAlpha(1.0f, 0.5f, false);
        string comboStr = comboCt.ToString();   //comboCt를 문자열로 받음
        comboUI.text = "X" + comboStr;  // 콤보 출력
    }

    public void cbCC()
    {
        comboCt = 0;    // 콤보 초기화
        nprintCombo();
    }
    public void nprintCombo()
    {
        comboEnd = true;
        //comboUI.text = "";  // 콤보 텍스트 없앰
        comboUI.CrossFadeAlpha(0.0f, 0.5f, false);
    }
}
