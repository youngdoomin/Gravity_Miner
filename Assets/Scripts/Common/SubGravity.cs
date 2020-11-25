using System.Collections;
using UnityEngine;

public class SubGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    public float divide;
    public int knock = 5;
    public int minimumSp = 10;
    bool dontLoop;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
            //rb.velocity = new Vector2(0, GameManager.Instance.sp);
            //rb.MovePosition(transform.position + transform.up * GameManager.Instance.sp * Time.deltaTime);
            //Vector2 movement = new Vector2(0, 1);
            //rb.AddForce(movement * GameManager.Instance.sp);
            GameManager.Instance.killLoop = false;
            GameManager.Instance.reaction = false;

            if (GameManager.Instance.sp < minimumSp)
                GameManager.Instance.sp += Time.fixedDeltaTime * PGravity.power / divide;

            else if (GameManager.Instance.sp < GameManager.Instance.speedLock)
                GameManager.Instance.sp += Time.fixedDeltaTime * GameManager.Instance.sp / divide;


            if (Input.GetKey(KeyCode.W) && GameManager.Instance.energy >= 0 )// && sp > 0 && sp < speedLock ) || (sp < PGravity.power))
            {
                GameManager.Instance.gravityVal = PGravity.power;
            }
            else if (Input.GetKey(KeyCode.S) && GameManager.Instance.energy >= 0)// && sp > 0)
            {
                GameManager.Instance.gravityVal = -PGravity.power;
            }


        }
        else if ((GameManager.Instance.kill || GameManager.Instance.tileBreak) && GameManager.Instance.sp != GameManager.Instance.speedLock) //적 처치, 블록 파괴 상태
        {
            transform.Translate(Vector2.down * Time.fixedDeltaTime * GameManager.Instance.sp * 10); //반대로 올라감
            //rb.velocity = new Vector2(0, -GameManager.Instance.sp);
            //rb.MovePosition(transform.position - transform.up * GameManager.Instance.sp * Time.deltaTime);
            //Vector2 movement = new Vector2(0, -1);
            //rb.AddForce(movement * GameManager.Instance.sp);
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





