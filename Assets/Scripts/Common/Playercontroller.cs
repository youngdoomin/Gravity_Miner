using System.Collections;
using UnityEngine;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax; // 플레이어 영역
}
public class Playercontroller : MonoBehaviour
{
    public Boundary boundary;
    public float movespeed = 7f;    //이동 속도
    public int invinTime;   // 무적 시간
    public static bool untouchable = false; // 무적 여부
    public static bool GroundDam = false;   // 바닥 데미지 여부
    public static int life; // 플레이어 체력
    public const int maxLife = 4;
    public static float energy;   // 중력 사용량
    public static bool kill = false;
    SpriteRenderer sR;

    private float xKnockB = 0.2f;
    private float yKnockB = 0.0f;
    private Rigidbody2D rb;

    public float jumpP;
    //public static float cjumpP;

    public Sprite[] Normal;
    public Sprite[] Walk;
    public Sprite Jump;
    public Sprite Damaged;
    public Sprite Kill;
    public Sprite Fall;

    private AudioSource PAudio;
    public AudioClip DamSound;
    private AudioSource EAudio;
    public AudioClip mallDie;
    public AudioClip itemSound;

    public static bool killLoop = false;
    public static bool comboActive = false;
    //bool standSwitch = true;
    // Use this for initialization


    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        //cjumpP = jumpP;
        rb = GetComponent<Rigidbody2D>();
        PAudio = gameObject.AddComponent<AudioSource>();
        PAudio.clip = DamSound;
        PAudio.loop = false;
        EAudio = gameObject.AddComponent<AudioSource>();
        EAudio.clip = mallDie;
        EAudio.loop = false;

        life = maxLife;
        energy = PGravity.fenergy;
        untouchable = false; // 무적 여부
        GroundDam = false;   // 바닥 데미지 여부
        kill = false;
        killLoop = false;
        comboActive = false;
    }


    void Update()
    {
        if (GroundDam)
        {
            OnDamaged();

        }
        if (kill && !killLoop)
        {
            //sR.sprite = Kill;  // 적 없애는 스프라이트
            EAudio.Play();
            killLoop = true;
        }
        /*
        if (PGravity.bJumped == true)
        {
            sR.sprite = Jump;

        }
        if (untouchable == false && (SubGravity.sp == 0 || PGravity.bJumped == true))
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
        }*/

    }
    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalMove, 0);
        if (!FollowPlayer.shake)
            rb.velocity = movement * movespeed;
        rb.position = new Vector2
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),  //X
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)    //Y
            );

        /*
        if (ColliderDetect.platformDetect == false && untouchable == false)//ColliderDetect.fallWaited == true && ColliderDetect.platformDetect == false && untouchable == false)
        {

            sR.sprite = Fall;
            //SubGravity.sp = Gravity.l;
        }
        if(ColliderDetect.platformDetect == true && standSwitch == true)
            StartCoroutine(Stand());
        */
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //PGravity.bJumped = false;

        if (collision.gameObject.tag == "Enemy" && !untouchable && !kill)
        {
            if (Block_special.shieldOn)
            {
                Block_special.shieldOn = false;
                Forced(collision.transform.position, xKnockB * 3);
            }
            else
            {
                OnDamaged();
                Forced(collision.transform.position, xKnockB);
            }

        }

        else if (collision.gameObject.tag == "jam" || collision.gameObject.tag == "item")
        {
            PAudio.clip = itemSound;
            PAudio.Play();
        }
    }

    public void OnDamaged()
    {
        life--;
        this.gameObject.BroadcastMessage("ParticlePlay");
        SubGravity.sp = 0;
        UIBar.inviC = true;
        Combo.cbCC();
        FollowPlayer.shake = true;
        HPManager.TakeDamage(life);
        sR.sprite = Damaged;
        GroundDam = false;
        PAudio.Play();
        if (life == 0)
        {
            StartCoroutine(Death());
        }
        StartCoroutine(Wait());
    }
    void Forced(Vector2 targetPos, float xKB)
    {
        //        gameObject.layer = 11;
        float dirc = transform.position.x - targetPos.x > 0 ? xKB : -xKB;   // 적 기준 좌우에 따라 x축이 다르게 넉백
        rb.AddForce(new Vector2(dirc, yKnockB), ForceMode2D.Force);
    }

    IEnumerator Wait()
    {

        untouchable = true;

        //SubGravity.sp = Gravity.l;
        int count = 0;
        while (count < invinTime)
        {


            if (count % 2 == 0) // 깜박이게 함
                sR.color = new Color(1, 1, 1, 0.4f);
            else
                sR.color = new Color(1, 1, 1, 0.8f);

            yield return new WaitForSeconds(0.2f);

            count++;
        }
        sR.color = new Color(1, 1, 1, 1);
        sR.sprite = Fall; // 기본 스프라이트
        untouchable = false; // 무적시간 종료

        //       gameObject.layer = 10;
        /*if (ColliderDetect.platformDetect == true)
            StartCoroutine(Stand());
        else
            sR.sprite = Fall; // 기본 스프라이트
        untouchable = false; // 무적시간 종료
        */
    }

    IEnumerator EnemyKill(GameObject enemy)
    {
        if (SubGravity.sp == SubGravity.speedLock)
        {
            SubGravity.sp = Gravity.k; // 내려가지 않게 함
            kill = true;
            Combo.comboCt++; // 콤보 증가
            Combo.i = 0;
            FollowPlayer.shake = true; // 화면 흔들림 켬
            EnemySpawner.EnemyCt--;
            Playercontroller.energy = PGravity.fenergy; // 중력량 증가
            Score.scoreCt += Combo.comboCt % 3 == 0 ? 200 : 100; // 기본 점수 100증가 3콤보 배수마다 200 증가
            comboActive = true;
            enemy.SendMessage("EnemyExDel");
            yield return null;

        }
    }
    IEnumerator Death()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(1);
        var uiPopup = GameObject.FindGameObjectWithTag("UI_Controller");
        uiPopup.SendMessage("OpenPopup", 1);
    }
}
/*
IEnumerator Stand()
{
    standSwitch = false;
    int j = 0;
    int i = 0;
        while (ColliderDetect.platformDetect == true && untouchable == false)
        {
            i++;
            sR.sprite = Normal[i % 2];

            yield return new WaitForSeconds(0.3f);

            while ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && ColliderDetect.platformDetect == true && untouchable == false)
            {
                j++;
                sR.sprite = Walk[j % 4];
                yield return new WaitForSeconds(0.1f);
            }
        }
    standSwitch = true;



}
*/

/*
    void Die()
    {
        isDie = true;

        rigidbody.velocity = Vector2.zero;

        BoxCollider2D[] colls = gameObject.GetComponents<BoxCollider2D>();
        colls[0].enabled = false;
        colls[1].enabled = false;

        Vector2 dieVelocity = new Vector2(0, 10f);
        rigidbody.AddForce(dieVelocity, ForceMode2D.Impulse);

        Invoke("RestartStage", 2f);


    }


    public static void Restartstage()
    {
        Time.timeScale = 0f;

        //SceneManager.LoadScene Scene(stageLevel, LoadSceneMode.Single);
    }
    */
