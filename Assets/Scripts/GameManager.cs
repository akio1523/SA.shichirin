using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public Text scoreText;

    private int countdownMinutes = 3;
    private float countdownSeconds;

    private float sum = 0;

  

    // Start is called before the first frame update
    void Start()
    {
        countdownSeconds = countdownMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {   
        //以下パクリ
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timerText.text = "タイム "+span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0)
        {
            // 0秒になったときの処理
        }

    }

    public void RefreshScoreText(float score)
    {
        sum += score;
        scoreText.GetComponent<Text>().text = "スコア " + sum.ToString();
    }
}
