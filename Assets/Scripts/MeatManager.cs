using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MeatManager : MonoBehaviour
{
    public GameObject GameManager;
    public Sprite[] meatPicture = new Sprite[4];
    public Sprite[] scorePicture = new Sprite[4];

    // 肉の色ごとのスコアを表すfloat型の配列
    public float[] meatScore = new float[4];

    // 肉の色のインデックスを表すint型の変数
    private int colorIndex;

    // 肉の色が変わる間隔を表すfloat型の変数
    public float colorChangeTime;

    private float score;

    private float grillMeatSeconds;
    // Start is called before the first frame update
    void Start()
    {
        // FindObjectOfTypeメソッドでGameManagerオブジェクトのコンポーネントへの参照を取得する
        //GameManager = FindObjectOfType<GameManager>();

        // colorIndexを0に初期化する
        colorIndex = -1;

        grillMeatSeconds = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        grillMeatSeconds += Time.deltaTime;

        if(grillMeatSeconds<5f) { }
    }

    private void ChangeColor()
    {
        colorIndex++;
        gameObject.GetComponent<Image>().sprite = meatPicture[colorIndex];
        //gameObject.GetComponent<Image>().sprite = scorePicture[colorIndex]; こっちは別のメソッドで生成？壁打ちを参照

    }
}
