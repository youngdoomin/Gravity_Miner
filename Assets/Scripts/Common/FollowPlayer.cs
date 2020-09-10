using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    public Vector3 offset; // 변위
    private float offsetY = 13.5f;
    public float shakeTime = 10.0f; // 흔들리는 시간
    private float shakeT; // 시간 초기값 받음
    public float shakeStr = 10.0f; // 흔들리는 정도
    public static bool shake = false;


    void Start()
    {
        shakeT = shakeTime;
        offset.y = offset.y - offsetY;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.y = player.position.y + offsetY;

        if (shake == true && SlowmotionManager.isPaused == false)
        {
            transform.position = new Vector3(Random.Range(-shakeStr, shakeStr), position.y + Random.Range(-shakeStr, shakeStr), -10); // 그 범위 안에서 랜덤하게 흔들림
            shakeTime -= Time.deltaTime; // 시간 감소
            if (shakeTime <= 0)
            {
                shake = false;
                shakeTime = shakeT; // 시간 초기화
            }
        }
        else
        { transform.position = new Vector3(0, position.y, -10); }// 원래 자리로
    }

    IEnumerator StartOffset()
    {
        for (int i = 0; i < -offset.y; i++)
        {
            offsetY--;
            yield return new WaitForSeconds(0.04f);
        }
    }

    IEnumerator EndOffset()
    {
        offsetY = (int)offset.y;
        yield return null;
    }

}