using UnityEngine;

public class EnemyDel : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)  // 다른 오브젝트가 나가면
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemySpawner.EnemyCt--;
        }
        Destroy(other.gameObject); // 다른 오브젝트 파괴
    }
}
