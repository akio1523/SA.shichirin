using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeatManager : MonoBehaviour
{
    public GameObject GameManager;
    public Sprite[] meatPicture = new Sprite[4];
    public Sprite[] scorePicture = new Sprite[4];

    // 肉の色ごとのスコアを表すfloat型の配列
    public float[] meatScore = new float[4];

    public float score;

    private float grillMeatSeconds;
    private int colorIndex;

    private GameObject Shichirin;
    // Start is called before the first frame update
    void Start()
    {
      

        grillMeatSeconds = 0;
        colorIndex=-1;
        Shichirin = GameObject.Find("ImageShichirin");
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (grillMeatSeconds < 5f && colorIndex != 0)
        {
            colorIndex = 0;
            ChangeColor(colorIndex);
        }
        else if (grillMeatSeconds < 7f && colorIndex != 1)
        {
            colorIndex = 1;
            ChangeColor(colorIndex);
        }
        else if (grillMeatSeconds < 8.5f && colorIndex != 2)
        {
            colorIndex = 2;
            ChangeColor(colorIndex);
        }
        else if (grillMeatSeconds < 10f && colorIndex != 3)
        {
            colorIndex = 3;
            ChangeColor(colorIndex);
        }
        else if (grillMeatSeconds >= 10f)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeColor(int colorIndex)
    {
       
        
            gameObject.GetComponent<Image>().sprite = meatPicture[colorIndex];
            //gameObject.GetComponent<Image>().sprite = scorePicture[colorIndex]; こっちは別のメソッドで生成？壁打ちを参照
            score = meatScore[colorIndex];
            //scorePicture[colorIndex];
            //Debug.Log("カラーインデックスは" + colorIndex);
        
    }


    public void TouchMeat()
    {
        GameManager.GetComponent<GameManager>().RefreshScoreText(score);
    }
}
