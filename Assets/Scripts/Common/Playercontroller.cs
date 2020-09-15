using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
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
    public static bool untouchable; // 무적 여부
    public static bool GroundDam; // 바닥 데미지 여부
    public static int life; // 플레이어 체력
    public const int maxLife = 4;
    public static float energy;   // 중력 사용량
    public static bool kill;
    SpriteRenderer sR;

    private float xKnockB = 0.2f;
    private float yKnockB = 0.0f;
    private Rigidbody2D rb;

    //public Sprite[] Normal;
    //public Sprite[] Walk;
    //public Sprite Jump;
    //public Sprite Kill;
    public Sprite Damaged;
    public Sprite Fall;

    public static bool killLoop;
    public static bool comboActive;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

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
        {OnDamaged();}
        if (kill && !killLoop)
        {
            SoundManager.instance.PlaySE(SoundManager.instance.kill);
            killLoop = true;
        }
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
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !untouchable && !kill && collision.gameObject.GetComponent<SpriteRenderer>().color.a != 0)
        {
            if (Block_special.shieldOn)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(BlinkEf(collision.gameObject));
                Block_special.shieldOn = false;
            }
            else
            {
                OnDamaged();
                Forced(collision.transform.position, xKnockB);
            }

        }

        else if (collision.gameObject.tag == "jam" || collision.gameObject.tag == "item")
        {SoundManager.instance.PlaySE(SoundManager.instance.lootItem);}
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
        SoundManager.instance.PlaySE(SoundManager.instance.damage);
        if (life == 0)
        { StartCoroutine(Death()); }
        StartCoroutine(Wait());
    }
    void Forced(Vector2 targetPos, float xKB)
    {
        float dirc = transform.position.x - targetPos.x > 0 ? xKB : -xKB;   // 적 기준 좌우에 따라 x축이 다르게 넉백
        rb.AddForce(new Vector2(dirc, yKnockB), ForceMode2D.Force);
    }

    IEnumerator Wait()
    {
        untouchable = true;
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
    }

    IEnumerator EnemyKill(GameObject enemy)
    {
        if (SubGravity.sp == SubGravity.speedLock)
        {
            SubGravity.sp = 0; // 내려가지 않게 함
            kill = true;
            Combo.comboCt++; // 콤보 증가
            Combo.i = 0;
            FollowPlayer.shake = true; // 화면 흔들림 켬
            EnemySpawner.EnemyCt--;
            energy = PGravity.fenergy; // 중력량 증가
            Score.scoreCt += Combo.comboCt % 3 == 0 ? 200 : 100; // 기본 점수 100증가 3콤보 배수마다 200 증가
            comboActive = true;
            enemy.SendMessage("EnemyExDel");
            yield return null;
        }
    }

    IEnumerator BlinkEf(GameObject obj)
    {
        var waitTime = 0.2f;
        var total = 0.0f;
        var objSpr = obj.GetComponent<SpriteRenderer>();
        while (total < 3)
        {
            if (objSpr.enabled) { objSpr.enabled = false; }
            else { objSpr.enabled = true; }
            yield return new WaitForSeconds(waitTime);
            total += waitTime;
        }
    }

    IEnumerator Death()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(1);
        var uiPopup = GameObject.FindGameObjectWithTag("UI_Controller");
        uiPopup.SendMessage("OpenPopup", 2);
    }
}
