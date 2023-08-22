using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public Text scoreText;
    public GameObject TextGameOver;

    private int countdownMinutes = 1;
    private float countdownSeconds;

    private float currentScore = 0;

  

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        countdownSeconds = countdownMinutes * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //以下パクリ
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timerText.text = "タイム "+span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0) GameOver();
        
        

    }

    public void RefreshScoreText(float score)
    {
        currentScore += score;
        scoreText.GetComponent<Text>().text = currentScore + " P".ToString();
    }

    private void GameOver()
    {
        
        Time.timeScale = 0; 

        PlayerPrefs.SetFloat("CURRENTSCORE", currentScore);
        TextGameOver.SetActive(true);

        Debug.Log("今回のスコア" + currentScore);
        if (PlayerPrefs.GetFloat("BESTSCORE", 0) < currentScore)
        {
            PlayerPrefs.SetFloat("BESTSCORE", currentScore);
            PlayerPrefs.Save();

            Debug.Log("bestスコア" + PlayerPrefs.GetFloat("BESTSCORE", 0));//飛ばしてるっぽい？
            
        }

        if (PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue) > currentScore)
        {
            PlayerPrefs.SetFloat("WORSTSCORE", currentScore);
            PlayerPrefs.Save();
            Debug.Log("worstスコア" + PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue));
        }

        PlayerPrefs.Save();

        
        StartCoroutine(GoToGameOverScene()); // 追加：コルーチンを使う
        Debug.Log("コルーチン開始");

    }

    private IEnumerator GoToGameOverScene()  //まるまる変更
    {
        yield return new WaitForSecondsRealtime(3); // Time.timeScaleの影響を受けない待機
        SceneManager.LoadScene("GameOverScene");
        Debug.Log("ゲームオーバー画面へ移動");

    }
}
