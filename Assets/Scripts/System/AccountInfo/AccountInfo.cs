using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Account", menuName = "Assets/Account")]
public class AccountInfo : ScriptableObject
{
    public int hiScore;
    public bool tutorialFinished = false;

    public void NewHiScore(int score)
    {
        if (score > hiScore)
        {
            hiScore = score;
            Debug.Log("최고기록 갱신!");
        }
        else
        {
            Debug.Log("현재 점수가 하이스코어보다 높지 않습니다");
        }
    }


    
}
