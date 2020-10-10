using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    private Text pressAnyB;
    private bool startflag;
    //public Image fadeout;

    private Animator animator;
    public GameObject platform;
    public GameObject background;
    public GameObject title;
    public MeshRenderer[] bgRender;

    private int step = 0;
    public const int _JUMP = 1;
    public const int _DESPLAT = 2;
    public const int _BGUP = 3;
    public const int _FADEOUT = 4;

    public float startDuration;
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        step = 0;
        pressAnyB = GameObject.Find("Press").GetComponent<Text>();
    }
    void Update()
    {
        if (Time.time % 2 >= 1)
        { pressAnyB.CrossFadeAlpha(1.0f, 0.5f, false); }
        else
        { pressAnyB.CrossFadeAlpha(0.0f, 0.5f, false); }

        if (Input.GetKeyDown(KeyCode.Return) && Time.time != 0)
        { startflag = true; }

        if (startflag == true)
        {
            {
                GameStart();
                startDuration += Time.deltaTime;
            }
        }

        void GameStart()
        {
            switch (step)
            {
                case 0:
                    step++;
                    pressAnyB.enabled = false;
                    break;

                case _JUMP:
                    background.transform.Translate(Vector2.up * Time.deltaTime * 30 * (startDuration - 0.4f));
                    title.transform.Translate(Vector2.up * Time.deltaTime * 720 * (startDuration - 0.4f));
                    animator.SetTrigger("Drill");
                    if (startDuration > 0.8f)
                    { step++; }
                    break;

                case _DESPLAT:  //발판 파괴
                    platform.BroadcastMessage("Destruct");
                    //Destroy(platform.gameObject);
                    var cam = GameObject.FindGameObjectWithTag("MainCamera");
                    cam.SendMessage("StartOffset");
                    step++;
                    break;

                case _BGUP: // 배경 올라옴
                    background.transform.Translate(Vector2.up * Time.deltaTime * 30 * (startDuration - 0.4f));
                    title.transform.Translate(Vector2.up * Time.deltaTime * 720 * (startDuration - 0.4f));
                    if (background.transform.position.y > 0)
                    {
                        background.transform.position = new Vector3(background.transform.position.x, 0, background.transform.position.z);
                        step++;
                    }
                    break;

                case _FADEOUT:
                    //background.transform.Translate(Vector2.up * Time.deltaTime * 30);
                    for (int i = 0; i < bgRender.Length; i++)
                    {
                        bgRender[i].material.mainTextureOffset += Vector2.down * (startDuration - 0.4f) / 100;
                    }
                    if (startDuration > 2f)
                    {/*
                    if (fadeout.color.a <= 1)
                    {fadeout.color += new Color(0, 0, 0, 1f) * Time.deltaTime;}
                    else
                    {
                        step++;
                        SceneManager.LoadScene("Flower_Rain", LoadSceneMode.Single);
                    }
                */
                        step++;
                        SoundManager.instance.PlayIngameLoop();
                        SceneManager.LoadScene("Flower_Rain", LoadSceneMode.Single);
                    }
                    break;
            }
        }
    }
}

