using UnityEngine;

public class PGravity : MonoBehaviour
{
    //public static bool bJumped;
    public static float power = 10f; //움직이는 속도
    public static bool screenFilter = false;
    public const float fenergy = 20;  // energy 변수 초기값 저장하는 변수(중력 최댓값)
    private float reduEner = 10f;  //중력 사용시 1초마다 줄어드는 정도
    //private GameObject trObj;
    void Start()
    {
        power = 10f; //움직이는 속도
        screenFilter = false;
    }
    void UseSkill()
    {
        Playercontroller.energy -= reduEner * Time.deltaTime;   // 중력 사용시 에너지 감소
        screenFilter = true;

    }

    void Update()
    {
        if (Playercontroller.energy > 0) // 에너지가 다 닳지 않으면
        {
            if (Input.GetKey(KeyCode.W) && SubGravity.sp > 0 || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) //wasd중 아무키나 누르면
            {
                AudioMixerManager.Instance.Setlowpass(AudioMixerManager.Instance.lowpassOn);
                UseSkill(); // 에너지 감소 함수 호출
                if (Input.GetKey(KeyCode.A)) //방향키 별로 다른 방향으로 이동시킴
                {
                    transform.Translate(Vector2.left * Time.deltaTime * power);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector2.right * Time.deltaTime * power);
                }
                else if (Input.GetKey(KeyCode.W) && SubGravity.sp > 0)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * power);
                    //SubGravity.sp += Time.deltaTime * power;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Score.scorecap = true;
                    transform.Translate(Vector2.down * Time.deltaTime * power);
                    //SubGravity.sp -= Time.deltaTime * power; 

                    if (!Input.GetKeyDown(KeyCode.S)) // S버튼을 뗐을시
                    {
                        SubGravity.sp = SubGravity.sp < 0 ? Gravity.l : SubGravity.sp;
                        Score.scorecap = false;
                    }

                }

            }
            else
            {
                OutPower();
                AudioMixerManager.Instance.Setlowpass(AudioMixerManager.Instance.lowpassOff);
            }
        }
        else
        {
            OutPower();
        }
        /*
     if (Input.GetKeyDown(KeyCode.Space) && SubGravity.sp == 0)
     {
         Jumped();
         bJumped = false;
     }
     */
    }
    void OutPower()
    {
        screenFilter = false;
    }
}
/*
public void Jumped()
{

    for(int i = 0;i < Playercontroller.cjumpP;i++)
    transform.Translate(Vector2.down * Time.deltaTime * i);
}
*/
