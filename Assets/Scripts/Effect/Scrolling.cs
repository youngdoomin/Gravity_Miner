using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed = 8f;    //스크롤 스피드
    private Material Background_mat;    //메터리얼 참조
    void Start()
    {
        Background_mat = GetComponent<Renderer>().material;     //메터리얼 참조 획득
        MeshCollider collider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        
        Vector2 offset = Background_mat.mainTextureOffset;  //현재 메터리얼의 offset값 획득
        
        if (SubGravity.sp != 0)     //offset값 갱신
        {
            offset.Set(0, offset.y - (SubGravity.sp / speed) * Time.deltaTime);   //offset 값 적용, 플레이어 속도만큼 y축으로 빨리 움직임
            Background_mat.mainTextureOffset = offset;
        }
    }
}
