
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
    public GameObject ImageMeat;
    public Sprite rawMeatPicture;

    public GameObject Shichirin;
    private GameObject Meat;
    public GameObject meatPrefab;
    public GameObject GameManager;
    public GameObject scoreTextPrefab;


    private bool hasMeat; // 
    private bool isGrilling; // フラグ用の変数

    public float score;




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

        if (collision.gameObject.tag == "Shichirin" && hasMeat&& !isGrilling)　//コルーチンで大量に生成されるのでグリルミートメソッド用のブール変数を用意
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

    private IEnumerator GrillMeat()
    {   

        isGrilling = true; // フラグtrueにする
        yield return new WaitForSeconds(0.5f);

        
        Meat = Instantiate(meatPrefab, Shichirin.transform);
        
        //meat.transform.SetParent(gameObject.transform, false);
        //Meat.transform.position = Shichirin.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);

        ImageMeat.SetActive(false);
        hasMeat = false;
       
        isGrilling = false; // フラグをfalseにする
    }

    public void GetScoreTextObject(int colorIndex, float score)
    {
        GameManager.GetComponent<GameManager>().RefreshScoreText(score);

        // コルーチンを呼び出す
        StartCoroutine(CreateScoreText(colorIndex, score));
    }

    // スコアテキストを生成するコルーチン(スコア反映とスコアテキストのずれ修正)
    private IEnumerator CreateScoreText(int colorIndex, float score)
    {
        GameObject scoreText = Instantiate(scoreTextPrefab);
        scoreText.transform.SetParent(gameObject.transform, false);
        scoreText.transform.position = transform.position;
        Debug.Log("スコアテキスト生成");
        if (colorIndex == 1) scoreText.GetComponent<Text>().text = "+" + score + "P".ToString();
        else scoreText.GetComponent<Text>().text = score + "P".ToString();
        Debug.Log("スコアテキストの点数は" + score);
        Rigidbody2D scoreTextRb = scoreText.GetComponent<Rigidbody2D>(); // scoreTextのRigidbody2Dを取得
        scoreTextRb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse); // 上方向に5fの力を加える

        // 0.5秒待つ
        yield return new WaitForSeconds(0.5f);

        // スコアテキストを破棄する
        Destroy(scoreText);
    }
}

    

    

   
    