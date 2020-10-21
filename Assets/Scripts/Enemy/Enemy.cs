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
    Animator animator;
    const int PLAYER = 8;
    const int ENEMY = 9;  //레이어마스크
     

    private void Start()
    {
        mole = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        deathEf = childEf.GetComponent<SpriteRenderer>();
        childEf.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var player = GameObject.FindGameObjectWithTag("Playerbody");
            player.SendMessage("EnemyKill", this.gameObject);
        }
    }

    void Update()
    {
        if (GameManager.Instance.untouchable == true) // 무적시간일 경우
            Physics2D.IgnoreLayerCollision(PLAYER, ENEMY, true);  //충돌 무시
        else
            Physics2D.IgnoreLayerCollision(PLAYER, ENEMY, false);  //충돌함
    }

    

    IEnumerator EnemyExDel()
    {
        Combo.comboCt++; // 콤보 증가
        Combo.i = 0;
        GameManager.Instance.energy = PGravity.fenergy; // 중력량 증가
        GameManager.Instance.scoreCt += Combo.comboCt % 3 == 0 ? 200 : 100; // 기본 점수 100증가 3콤보 배수마다 200 증가
        GameManager.Instance.comboActive = true;

        animator.enabled = false;
        childEf.SetActive(true);

        for (int i = 0; i < death.Length; i++)
        {
            deathEf.sprite = death[i];
            yield return new WaitForSeconds(0.2f);
        }
        childEf.SetActive(false);
        mole.color = new Color(1, 1, 1, 0);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
    }


}
