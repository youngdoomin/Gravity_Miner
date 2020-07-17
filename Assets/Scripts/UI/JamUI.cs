using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class JamUI : MonoBehaviour
{
    public Image[] jamArray;
    public static Image[] cJamArray;
    public static float waitTime = 0;
    public static int uiJamCt;
    public Sprite defaultSprite;
    public static bool delay = false;
    public static bool collect = false;
    public static bool collectTxt = false;
    public static bool repeat;
    // Start is called before the first frame update
    void Start()
    {
        cJamArray = jamArray;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }

    public static void spriteToUI(int amount)
    {
        if (repeat == false)
        {
            cJamArray[amount - 1].sprite = Jam_ReturnSprite.cJamUI;

        }
        if (amount >= cJamArray.Length)
        {
            Jam_ReturnSprite.jamCt = 0;
        }
    }



    IEnumerator Collect()
    {
        for (int i = 0; i < cJamArray.Length; i++)
        { cJamArray[i].sprite = defaultSprite; }
        delay = false;
        yield return null;

        delay = true;
        collect = true;
        collectTxt = true;
        yield return null;
    }
}
