using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject childEf;
    public Sprite[] death;
    public Sprite attack; // 공격 모션 스프라이트
    SpriteRenderer mole; // 기본 스프라이트
    SpriteRenderer deathEf;
    Animator animate;
    const int PLAYER = 8;
    const int ENEMY = 9;  //레이어마스크

    private void Start()
    {
        mole = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();
        deathEf = childEf.GetComponent<SpriteRenderer>();
        childEf.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        mole.sprite = attack; // 스프라이트 변경
        if(col.gameObject.tag == "Player")
        {
            var player = GameObject.FindGameObjectWithTag("Playerbody");
            player.SendMessage("EnemyKill", this.gameObject);
        }
    }


    void Update()
    {
        
    if (Playercontroller.untouchable == true) // 무적시간일 경우
        Physics2D.IgnoreLayerCollision(PLAYER, ENEMY, true);  //충돌 무시
        else
        Physics2D.IgnoreLayerCollision(PLAYER, ENEMY, false);  //충돌함
    }

    IEnumerator EnemyExDel()
    {
        // this.gameObject.BroadcastMessage("ParticlePlay");
        animate.enabled = false;
        childEf.SetActive(true);
        
        for(int i = 0; i < death.Length; i++)
        {
            deathEf.sprite = death[i];
            yield return new WaitForSeconds(0.2f);
        }
        deathEf.sprite = null;
        mole.sprite = null;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
        yield return null;
    }


}
