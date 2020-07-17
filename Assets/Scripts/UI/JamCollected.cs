using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JamCollected : MonoBehaviour
{
    public Text jColUI;

    void Start()
    {
        jColUI = GameObject.Find("Jam_Collect").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (JamUI.collectTxt == true)    
        {
            printText();
            StartCoroutine(JamTranspacy());
        }
        else
        {
            nprintText();
        }
    }

    void printText()
    {
        jColUI.text = "Mining Bonus!";
        
    }

    public void nprintText()
    {
        jColUI.text = ""; 
    }
    IEnumerator JamTranspacy()
    {
        //jColUI.CrossFadeAlpha(1.0f, 0.5f, false);
        jColUI.color = new Color(1, 1, 1, Time.time % 3);
        yield return new WaitForSeconds(3);
        JamUI.collectTxt = false;
    }
}
