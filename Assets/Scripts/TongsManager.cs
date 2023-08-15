using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TongsManager : MonoBehaviour, IDragHandler,IPointerUpHandler

{
    public GameObject ImageMeat;
    public Sprite rawMeatPicture;

    public GameObject Shichirin;
    public GameObject Meat;
    private bool hasMeat; // トングが肉のイメージを持っているかどうかの変数



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
        if(collision.gameObject.tag=="Plate"&&!hasMeat)
        {
            ImageMeat.SetActive(true);
            hasMeat = true;
        }
        
        if (collision.gameObject.tag == "Shichirin" && hasMeat)
        {
            Shichirin.GetComponent<ShichirinManager>().AddMeat();
            ImageMeat.SetActive(false);
            hasMeat = false;
        }

        


    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meat" && !hasMeat)
        {
            Shichirin.GetComponent<ShichirinManager>().TouchMeat();
            
        }

    }*/

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        // ドラッグ終了時のマウスポインターの位置を取得する
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;

        // その位置にあるオブジェクトを検出する
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // オブジェクトが存在し、七輪であれば
        if (hit.collider != null && hit.collider.gameObject.tag == "Shichirin")
        {
            // AddMeatメソッドを呼び出す
            Shichirin.GetComponent<ShichirinManager>().AddMeat();
            ImageMeat.SetActive(false);

        }
    }
}
