using System.Collections;
using UnityEngine;

public class SubGravity : MonoBehaviour
{
    public float divide;
    public int knock = 5;
    bool dontLoop;

    void Update()
    {
        if (GameManager.Instance.sp > GameManager.Instance.speedLock)
        {
            GameManager.Instance.sp = GameManager.Instance.speedLock;
            if (dontLoop == false)
            {
                SoundManager.instance.PlaySE(SoundManager.instance.fullSpeed);
                dontLoop = true;
            }
        }
        else
        { dontLoop = false; }
    }
    private void FixedUpdate()
    {

        if (!GameManager.Instance.kill && !GameManager.Instance.tileBreak) //평소 상태
        {
            transform.Translate(Vector2.up * Time.fixedDeltaTime * GameManager.Instance.sp);
            GameManager.Instance.killLoop = false;
            GameManager.Instance.reaction = false;

            if(GameManager.Instance.sp < PGravity.power)
                GameManager.Instance.sp += Time.fixedDeltaTime * PGravity.power / divide;

            else if (GameManager.Instance.sp < GameManager.Instance.speedLock)
                GameManager.Instance.sp += Time.fixedDeltaTime * GameManager.Instance.sp / divide;


            if (Input.GetKey(KeyCode.W) && GameManager.Instance.energy >= 0 )// && sp > 0 && sp < speedLock ) || (sp < PGravity.power))
            {
                GameManager.Instance.gravityVal = PGravity.power;
            }
            else if (Input.GetKey(KeyCode.S) && GameManager.Instance.energy >= 0)// && sp > 0)
            {
                //sp += Time.fixedDeltaTime * PGravity.power / divide;
                GameManager.Instance.gravityVal = -PGravity.power;
            }


        }
        else if ((GameManager.Instance.kill || GameManager.Instance.tileBreak) && GameManager.Instance.sp != GameManager.Instance.speedLock) //적 처치, 블록 파괴 상태
        {
            transform.Translate(Vector2.down * Time.fixedDeltaTime * GameManager.Instance.sp * 10); //반대로 올라감
            GameManager.Instance.sp += knock * Time.fixedDeltaTime;
            StartCoroutine(WaitReact());
        }
        else { 
            GameManager.Instance.kill = false;
            GameManager.Instance.tileBreak = false;
        }


    }
    IEnumerator WaitReact()
    {
        GameManager.Instance.reaction = true;
        yield return new WaitForSeconds(0.35f);
        GameManager.Instance.sp = 1.0f;
        GameManager.Instance.kill = false; //초기화

        GameManager.Instance.tileBreak = false;
        //Debug.Log("Wait");
    }
}





