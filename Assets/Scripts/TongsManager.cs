
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class TongsManager : MonoBehaviour, IDragHandler

{
    public GameObject ImageMeat; // つかんだ肉の画像
    public Sprite rawMeatPicture; // 生肉の画像

    public GameObject Shichirin; // 七輪のオブジェクト
    private GameObject Meat; // 置いた肉のオブジェクト
    public GameObject meatPrefab; // 置く肉のプレハブ
    public GameObject GameManager; 
    public GameObject scoreTextPrefab; // スコアテキストのプレハブ

    private bool hasMeat; // 肉をつかんでいるかどうかのフラグ
    private bool isGrilling; // 肉をグリルに置いているかどうかのフラグ

    private float score; // スコア




    void Start()
    {
        ImageMeat.SetActive(false);
        hasMeat = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 tongsPos = Camera.main.ScreenToWorldPoint(eventData.position);
        tongsPos.z = 0;
        transform.position = tongsPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Plate" && !hasMeat)
        {
            StartCoroutine(PickUpMeat()); 
        }

        if (collision.gameObject.tag == "Shichirin" && hasMeat&& !isGrilling)　//コルーチンで大量に生成されるため、グリルミートメソッド用のブール変数を用意
        {
            StartCoroutine(GrillMeat()); 
        }
    }
   

    // 一定時間後にImageMeatを表示するコルーチン
    private IEnumerator PickUpMeat()
    {
        yield return new WaitForSeconds(0.5f); 
        ImageMeat.SetActive(true); 
        hasMeat = true; 
        yield break;
    }

    // 一定時間後にImageMeatを非表示し、肉を七輪に生成するコルーチン
    private IEnumerator GrillMeat()
    {   

        isGrilling = true; // フラグtrueにする
        yield return new WaitForSeconds(0.5f);

        
        Meat = Instantiate(meatPrefab, Shichirin.transform);

        ImageMeat.SetActive(false);
        hasMeat = false;
       
        isGrilling = false; // フラグをfalseにする
    }


    //ミートマネージャーから呼び出されるメソッド
    public void GetScoreTextObject(int colorIndex, float score)
    {   
        //スコア表示
        GameManager.GetComponent<GameManager>().RefreshScoreText(score);

        // スコアテキスト生成のコルーチンを呼び出す
        StartCoroutine(CreateScoreText(colorIndex, score));
    }

    // スコアテキストの生成～破壊までのコルーチン
    private IEnumerator CreateScoreText(int colorIndex, float score)
    {
        GameObject scoreText = Instantiate(scoreTextPrefab);
        scoreText.transform.SetParent(gameObject.transform, false);
        scoreText.transform.position = transform.position;

            
        scoreText.GetComponent<Text>().text = score + "P".ToString();
        
       //スコアテキストのアニメーション
        Rigidbody2D scoreTextRb = scoreText.GetComponent<Rigidbody2D>(); // scoreTextのRigidbody2Dを取得
        scoreTextRb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse); // 上方向に5fの力を加える

        yield return new WaitForSeconds(0.5f);

        Destroy(scoreText);
    }
}

    

    

   
    