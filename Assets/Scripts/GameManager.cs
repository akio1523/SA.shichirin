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
    public GameObject TextGameOver;//�u�����I�v�̕���

    private int countdownMinutes = 1;
    private float countdownSeconds;

    private float currentScore = 0;

  

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;//�Q�[���I�[�o�[�Ŏ��Ԃ��~�܂�̂ŏ�����������
        countdownSeconds = countdownMinutes * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //���Ԃ̕\���i�l�b�g����p�N���j
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timerText.text = "�^�C�� "+span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0) GameOver();
        
        

    }

    //�g���O�}�l�[�W���[����Ăяo����郁�\�b�h
    public void RefreshScoreText(float score)
    {
        currentScore += score;
        scoreText.GetComponent<Text>().text = currentScore + " P".ToString();
    }

    private void GameOver()
    {
        //���Ԃ��~�߂�
        Time.timeScale = 0;

        //����̃X�R�A
        PlayerPrefs.SetFloat("CURRENTSCORE", currentScore);

        //�u�����I�v�̕����\��
        TextGameOver.SetActive(true);

        //�ō��_�̍X�V       
        if (PlayerPrefs.GetFloat("BESTSCORE", 0) < currentScore)
        {
            PlayerPrefs.SetFloat("BESTSCORE", currentScore);
            PlayerPrefs.Save();

                Debug.Log("best�X�R�A" + PlayerPrefs.GetFloat("BESTSCORE", 0));
            
        }

        //�Œ�_�̍X�V�iMaxValue�������悭�킩���ĂȂ��j
        if (PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue) > currentScore)
        {
            PlayerPrefs.SetFloat("WORSTSCORE", currentScore);
            PlayerPrefs.Save();

                Debug.Log("worst�X�R�A" + PlayerPrefs.GetFloat("WORSTSCORE", float.MaxValue));
        }

        PlayerPrefs.Save();

        // �R���[�`���ŃQ�[���I�[�o�[��ʂɑJ��
        StartCoroutine(GoToGameOverScene()); 
        

    }

    private IEnumerator GoToGameOverScene()  
    {
        yield return new WaitForSecondsRealtime(3); 
        SceneManager.LoadScene("GameOverScene");
        
    }
}
