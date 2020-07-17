using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [SerializeField]

    public Image[] heart; // static으로 표현하면 인스펙터에 표시가 안됨
    public Sprite fullHeart; // 
    public Sprite emptyHeart;

    public static Image[] stHeart;
    public static Sprite stEmptyHeart;
    public static Sprite stFullHeart;

    void Awake() //static 변수에다 인스펙터에서 받은 오브젝트, 스프라이트를 받음
    {
        stHeart = heart;
        stEmptyHeart = emptyHeart;
        stFullHeart = fullHeart;
    }

    public static void TakeDamage(int amount)
    {
        stHeart[amount].sprite = stEmptyHeart; // 체력 UI 변경
    }

    public static void Heal(int amount)
    {
        stHeart[amount - 1].sprite = stFullHeart;
    }

}
