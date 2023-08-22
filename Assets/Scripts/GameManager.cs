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
        //�ȉ��p�N��
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timerText.text = "�^�C�� "+span.ToString(@"mm\:ss");

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

        Debug.Log("����̃X�R�A" + currentScore);
        if (PlayerPrefs.GetFloat("BESTSCORE", 0) < currentScore)
        {
            PlayerPrefs.SetFloat("BESTSCORE", currentScore);
            PlayerPrefs.Save();

            Debug.Log("best�X�R�A" + PlayerPrefs.GetFloat("BESTSCORE", 0));//��΂��Ă���ۂ��H
            
        }

        if (PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue) > currentScore)
        {
            PlayerPrefs.SetFloat("WORSTSCORE", currentScore);
            PlayerPrefs.Save();
            Debug.Log("worst�X�R�A" + PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue));
        }

        PlayerPrefs.Save();

        
        StartCoroutine(GoToGameOverScene()); // �ǉ��F�R���[�`�����g��
        Debug.Log("�R���[�`���J�n");

    }

    private IEnumerator GoToGameOverScene()  //�܂�܂�ύX
    {
        yield return new WaitForSecondsRealtime(3); // Time.timeScale�̉e�����󂯂Ȃ��ҋ@
        SceneManager.LoadScene("GameOverScene");
        Debug.Log("�Q�[���I�[�o�[��ʂֈړ�");

    }
}
