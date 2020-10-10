using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIBar : MonoBehaviour
{
    public static bool inviP;
    public static bool inviC;
    public static float waitP = 0;
    public static float waitC = 0;
    private int j = 1;
    private int i = 1;
    public Bar_Value Pb; // 중력바
    public static Bar_Value CPb;

    private int a = 1;
    private int b = 1;
    public Bar_Value Sb; // 속도바
    public static Bar_Value CSb;

    private int c = 1;
    private int d = 1;
    public Bar_Value Cb; // 콤보바
    public static Bar_Value CCb;
    private float alphaObj = 0;

    private void Start()
    {
        GameManager.Instance.energy = PGravity.fenergy; // 중력량 증가

        CSb = Sb;
        CPb = Pb;
        CCb = Cb;
        Pb.BarValue = 50;
        while (GameManager.Instance.energy * i <= 100.0f) // 100이 넘을 때까지 곱함
        {
            i++;
        }
        j = (int)GameManager.Instance.energy * i - 100; // 거기서 초과함 값을 뺌

        while (GameManager.Instance.speedLock * a <= 100.0f)
        {
            a++;
        }
        b = (int)GameManager.Instance.speedLock * a - 100;

        while (Combo.timer * c <= 100.0f)
        {
            c++;
        }
        d = (int)Combo.timer * c - 100;
    }

    private void Update()
    {

        StartCoroutine(WaitInvisible(GameManager.Instance.energy));
    }
    void FixedUpdate()
    {
        int speedUI;
        int energyUI = (int)GameManager.Instance.energy;
        if (GameManager.Instance.kill == false || GameManager.Instance.tileBreak == false)
        {
            speedUI = (int)GameManager.Instance.sp;

        }
        else
        {
            speedUI = 0;
        }

        Pb.BarValue = energyUI * i - (j / 2); // 중력바 정도 변경
        if (GameManager.Instance.kill == false && GameManager.Instance.tileBreak == false)
        {
            Sb.BarValue = speedUI * a - (b / 2);
        }

        int cbUI = Combo.timer;
        if (Combo.i != 0)
        {
            Cb.BarValue = (cbUI - Combo.i) * c - (d / 2);

        }

    }
    IEnumerator WaitInvisible(float barVal)
    {
        yield return new WaitForSeconds(1);
        if (barVal == GameManager.Instance.energy && inviP == false)
        {

            waitP += 0.1f;
            yield return new WaitForSeconds(2f);
            if (waitP >= 1f)
            {
                inviP = true;
                //waitP = 1;
            }
        }
        else if (barVal != GameManager.Instance.energy)
        {

            waitP = 0;
            inviP = false;

        }
        
        if (Combo.comboEnd == true)
        {
            inviC = true;
            waitC += 0.1f;
        }
        else
        {
            inviC = false;
            waitC = 0;
        }
        
    }
}