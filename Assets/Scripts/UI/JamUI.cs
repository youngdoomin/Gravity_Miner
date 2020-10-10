using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class JamUI : MonoBehaviour
{
    public Image[] jamArray;
    public Sprite defaultSprite;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.cJamArray = jamArray;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }

    public static void spriteToUI(int amount)
    {
        if (GameManager.Instance.repeat == false)
        {
            GameManager.Instance.cJamArray[amount - 1].sprite = GameManager.Instance.cJamUI;

        }
        if (amount >= GameManager.Instance.cJamArray.Length)
        {
            GameManager.Instance.jamCt = 0;
        }
    }



    IEnumerator Collect()
    {
        for (int i = 0; i < GameManager.Instance.cJamArray.Length; i++)
        { GameManager.Instance.cJamArray[i].sprite = defaultSprite; }
        GameManager.Instance.delay = false;
        yield return null;

        GameManager.Instance.delay = true;
        GameManager.Instance.collect = true;
        GameManager.Instance.collectTxt = true;
        yield return null;
    }
}
