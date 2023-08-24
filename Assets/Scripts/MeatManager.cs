using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class MeatManager : MonoBehaviour
{
    private GameObject GameManager;
    private GameObject Shichirin;
    private GameObject Tongs;

    public Sprite[] meatPicture = new Sprite[4];//肉の色の画像
    public Sprite[] scorePicture = new Sprite[4];//←使ってないです
    public float[] meatScore = new float[4]; // 肉の焼き加減に伴うスコア

    private float score;
    private float grillMeatSeconds;//肉の焼き時間
    private int colorIndex;//肉の焼き加減に伴う番号

    private bool CantTransfer;//肉が焦げた時のフラグ

    // Start is called before the first frame update
    void Start()
    {
      

        grillMeatSeconds = 0;
        colorIndex=0;
        Shichirin = GameObject.Find("ImageShichirin");
        GameManager = GameObject.Find("GameManager");
        Tongs = GameObject.Find("Tongs");

        CantTransfer = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //肉が七輪の直下に配置されたら焼く時間の計測開始
        if (transform.IsChildOf(Shichirin.transform)) 
        { 
            grillMeatSeconds += Time.deltaTime;
        }

        //肉の焼き加減
        if (grillMeatSeconds < 5f )
        {
            colorIndex = 0;
            ChangeColor(colorIndex);
        }
        else if (grillMeatSeconds < 7f )
        {
            colorIndex = 1;
            ChangeColor(colorIndex);
        }
        else if (grillMeatSeconds < 8.5f )
        {
            colorIndex = 2;
            ChangeColor(colorIndex);
        }
        else //こげて取れない状態
        {
            colorIndex = 3;
            ChangeColor(colorIndex);
            Destroy(gameObject, 1.5f);

            if (!CantTransfer)//一回だけ↓の内容を呼び出すための条件式
            {
                Tongs.GetComponent<TongsManager>().GetScoreTextObject(colorIndex, score);
                CantTransfer = true;
            }
 
        }
        
    }

    //FixedUpdateに合わせて肉の色とスコアの変更を呼び出す
    private void ChangeColor(int colorIndex)
    {
        gameObject.GetComponent<Image>().sprite = meatPicture[colorIndex];
        score = meatScore[colorIndex];
    }

    //トングについた肉の画像を表示していない状態＆肉が焦げてないときにトングが肉に触ると
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tongs"&& collision.gameObject.tag != "Meat"&&colorIndex!=3)
        {
             //焼いた肉を取るコルーチン開始↓
            StartCoroutine(TransferGrilledMeat());

        }

    }
    public IEnumerator TransferGrilledMeat()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Tongs.GetComponent<TongsManager>().GetScoreTextObject(colorIndex, score);// ここでトングマネージャーからスコアを取得

    }

   
}
