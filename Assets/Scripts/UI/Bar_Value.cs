using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class Bar_Value : MonoBehaviour
{
    /*
    [Header("Title Setting")]
    public string Title;
    public Color TitleColor;
    public Font TitleFont;
    public int TitleFontSize = 10;
    */
    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]

    private Color BarAlertColor = Color.red;
    public Image bar, barBackground;
    private bool isInvinP = false;
    private bool isInvinC = false;

    //   private Text txtTitle;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100); //0에서 100까지 값으로 고정
            barValue = value;
            UpdateValue(barValue);

        }
    }

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        //        txtTitle = transform.Find("Text").GetComponent<Text>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
        //Destroy(SpAudio);

    }

    private void Start()
    {

        /*
        txtTitle.text = Title;
        txtTitle.color = TitleColor;
        txtTitle.font = TitleFont;
        txtTitle.fontSize = TitleFontSize;
        */

        bar.color = BarColor;
        barBackground.color = BarBackGroundColor;
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);


    }

    void UpdateValue(float val)
    {
        bar.fillAmount = val / 100;
        //txtTitle.text = Title + " " + val + "%";
        bar.color = BarColor;
        barBackground.color = BarBackGroundColor;
        if (GameManager.Instance.sp == GameManager.Instance.speedLock)
        {
            UIBar.CSb.bar.color = UIBar.CSb.BarAlertColor;
        }
    }


    private void Update()
    {
        if (UIBar.inviC == true && isInvinC == false)
        {
            UIBar.CCb.bar.CrossFadeAlpha(0.0f, 0.5f, false);
            isInvinC = true;
        }
        else if (GameManager.Instance.comboActive == true && isInvinC == true)
        {
            UIBar.CCb.bar.CrossFadeAlpha(1.0f, 0.5f, false);
            GameManager.Instance.comboActive = false;
            isInvinC = false;
        }
        if (UIBar.inviP == true)
        {
            UIBar.CPb.barBackground.CrossFadeAlpha(0.0f, 0.5f, false);
            UIBar.CPb.bar.CrossFadeAlpha(0.0f, 0.5f, false);
            isInvinP = false;
        }
        else if (UIBar.inviP == false && isInvinP == false && (UIBar.CPb.barBackground.color.a != 0 && UIBar.CPb.bar.color.a != 0))
        {
            UIBar.CPb.barBackground.CrossFadeAlpha(1.0f, 0.5f, false);
            UIBar.CPb.bar.CrossFadeAlpha(1.0f, 0.5f, false);
            isInvinP = true;
        }
    }

}