using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    
    public void GoGameScene()
    {
        SceneManager.LoadScene("GameScene");
        
    }
    public void GoHowToScene()
    {
        SceneManager.LoadScene("HowToScene");
    }
}
