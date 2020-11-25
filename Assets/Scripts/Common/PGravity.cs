using UnityEngine;

public class PGravity : MonoBehaviour
{
    public const float power = 100f; //움직이는 속도
    public const float fenergy = 9;  // energy 변수 초기값 저장하는 변수(중력 최댓값)
    public float reduEner = 15f;  //중력 사용시 1초마다 줄어드는 정도
    private Rigidbody2D rb;

    float shortTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void UseSkill()
    {
        GameManager.Instance.energy -= reduEner * Time.deltaTime;   // 중력 사용시 에너지 감소
        GameManager.Instance.screenFilter = true;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.energy > 0 && GameManager.Instance.life > 0 &&
            GameManager.Instance.sp > 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) // 에너지가 다 닳지 않으면
        {
            AudioMixerManager.Instance.Setlowpass(AudioMixerManager.Instance.lowpassOn);
            UseSkill(); // 에너지 감소 함수 호출
            shortTimer += Time.deltaTime;
            if(shortTimer > 0.03)
            {
                PushArrowKey();

            }
        }
        else
        { OutPower(); }
    }

    void PushArrowKey()
    {
        if (Input.GetKey(KeyCode.A)) //방향키 별로 다른 방향으로 이동시킴
        {
            rb.velocity = new Vector2(-power, 0);
            //rb.AddForce(-transform.right * power * Time.deltaTime, ForceMode2D.Impulse );

            //transform.Translate(Vector2.left * Time.deltaTime * power);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(power, 0);
            //transform.Translate(Vector2.right * Time.deltaTime * power); 
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, power);
            //transform.Translate(Vector2.up * Time.deltaTime * power); 
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //rb.MovePosition(transform.position + transform.up * power * Time.deltaTime);
            rb.velocity = new Vector2(0, -power);
            GameManager.Instance.scorecap = true;
            //transform.Translate(Vector2.down * Time.deltaTime * power);

            if (!Input.GetKeyDown(KeyCode.S)) // S버튼을 뗐을시
            { GameManager.Instance.scorecap = false; }
        }
        else
        { OutPower(); }
    }

    void OutPower()
    {
        GameManager.Instance.screenFilter = false;
        AudioMixerManager.Instance.Setlowpass(AudioMixerManager.Instance.lowpassOff);
        rb.velocity = new Vector2(0, 0);
        shortTimer = 0;
    }
}
