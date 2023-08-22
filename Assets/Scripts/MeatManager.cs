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
    public Sprite[] meatPicture = new Sprite[4];
    public Sprite[] scorePicture = new Sprite[4];

    // ���̐F���Ƃ̃X�R�A��\��float�^�̔z��
    public float[] meatScore = new float[4];

    private float score;
    private float grillMeatSeconds;
    private int colorIndex;

    
    

    private bool CantTransfer;
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
        if(transform.IsChildOf(Shichirin.transform))grillMeatSeconds+= Time.deltaTime;

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
        else 
        {
            colorIndex = 3;
            ChangeColor(colorIndex);
            Destroy(gameObject, 1.5f);

            if (!CantTransfer)
            {
                Tongs.GetComponent<TongsManager>().GetScoreTextObject(colorIndex,score);
                CantTransfer = true;
            }


           
        }
        
    }

    private void ChangeColor(int colorIndex)
    {
        gameObject.GetComponent<Image>().sprite = meatPicture[colorIndex];
        score = meatScore[colorIndex];
        
            
        
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tongs"&& collision.gameObject.tag != "Meat"&&colorIndex!=3)
        {
             // �����ŃX�R�A���擾
            StartCoroutine(TransferGrilledMeat());
            Debug.Log("transfer�R���[�`���J�n");
            Debug.Log("�J���[�C���f�b�N�X��" + colorIndex);
            Debug.Log("�X�R�A��" + score);


        }

    }
    public IEnumerator TransferGrilledMeat()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Tongs.GetComponent<TongsManager>().GetScoreTextObject(colorIndex, score);


    }

    


   
}
