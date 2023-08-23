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
    public GameObject TextGameOver;//「おわり！」の文字

    private int countdownMinutes = 1;
    private float countdownSeconds;

    private float currentScore = 0;

  

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;//ゲームオーバーで時間が止まるので初期化させる
        countdownSeconds = countdownMinutes * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //時間の表示（ネットからパクリ）
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timerText.text = "タイム "+span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0) GameOver();
        
        

    }

    //トングマネージャーから呼び出されるメソッド
    public void RefreshScoreText(float score)
    {
        currentScore += score;
        scoreText.GetComponent<Text>().text = currentScore + " P".ToString();
    }

    private void GameOver()
    {
        //時間を止める
        Time.timeScale = 0;

        //今回のスコア
        PlayerPrefs.SetFloat("CURRENTSCORE", currentScore);

        //「おわり！」の文字表示
        TextGameOver.SetActive(true);

        //最高点の更新       
        if (PlayerPrefs.GetFloat("BESTSCORE", 0) < currentScore)
        {
            PlayerPrefs.SetFloat("BESTSCORE", currentScore);
            PlayerPrefs.Save();

                Debug.Log("bestスコア" + PlayerPrefs.GetFloat("BESTSCORE", 0));
            
        }

        //最低点の更新（MaxValueが何かよくわかってない）
        if (PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue) > currentScore)
        {
            PlayerPrefs.SetFloat("WORSTSCORE", currentScore);
            PlayerPrefs.Save();

                Debug.Log("worstスコア" + PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue));
        }

        PlayerPrefs.Save();

        // コルーチンでゲームオーバー画面に遷移
        StartCoroutine(GoToGameOverScene()); 
        

    }

    private IEnumerator GoToGameOverScene()  
    {
        yield return new WaitForSecondsRealtime(3); 
        SceneManager.LoadScene("GameOverScene");
        
    }
}
