using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrintScore : MonoBehaviour
{
    public Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int score = (int)GameManager.Instance.scoreCt;
        scoreText.text = score.ToString("0000000");
    }
}
