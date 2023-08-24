using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDirector : MonoBehaviour
{

    public GameObject TextCurrentScore;
    public GameObject TextBestScore;
    public GameObject TextWorstScore;

    // Start is called before the first frame update
    void Start()
    {

        TextCurrentScore.GetComponent<Text>().text = PlayerPrefs.GetFloat("CURRENTSCORE", 0)+"P".ToString();

        TextBestScore.GetComponent<Text>().text = PlayerPrefs.GetFloat("BESTSCORE", 0) + "P".ToString();
        TextWorstScore.GetComponent<Text>().text = PlayerPrefs.GetFloat("WORSTSCORE", 0) + "P".ToString();

    }

    public void GoGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
