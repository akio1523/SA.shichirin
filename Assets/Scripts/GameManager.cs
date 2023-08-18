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
        //�ȉ��p�N��
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timerText.text = "�^�C�� "+span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0)
        {
            // 0�b�ɂȂ����Ƃ��̏���
        }

    }

    public void RefreshScoreText(float score)
    {
        sum += score;
        scoreText.GetComponent<Text>().text = "�X�R�A " + sum.ToString();
    }
}
