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

    public Sprite[] meatPicture = new Sprite[4];//���̐F�̉摜
    public Sprite[] scorePicture = new Sprite[4];//���g���ĂȂ��ł�
    public float[] meatScore = new float[4]; // ���̏Ă������ɔ����X�R�A

    private float score;
    private float grillMeatSeconds;//���̏Ă�����
    private int colorIndex;//���̏Ă������ɔ����ԍ�

    private bool CantTransfer;//�����ł������̃t���O

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
        //�������ւ̒����ɔz�u���ꂽ��Ă����Ԃ̌v���J�n
        if (transform.IsChildOf(Shichirin.transform)) 
        { 
            grillMeatSeconds += Time.deltaTime;
        }

        //���̏Ă�����
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
        else //�����Ď��Ȃ����
        {
            colorIndex = 3;
            ChangeColor(colorIndex);
            Destroy(gameObject, 1.5f);

            if (!CantTransfer)//��񂾂����̓��e���Ăяo�����߂̏�����
            {
                Tongs.GetComponent<TongsManager>().GetScoreTextObject(colorIndex, score);
                CantTransfer = true;
            }
 
        }
        
    }

    //FixedUpdate�ɍ��킹�ē��̐F�ƃX�R�A�̕ύX���Ăяo��
    private void ChangeColor(int colorIndex)
    {
        gameObject.GetComponent<Image>().sprite = meatPicture[colorIndex];
        score = meatScore[colorIndex];
    }

    //�g���O�ɂ������̉摜��\�����Ă��Ȃ���ԁ������ł��ĂȂ��Ƃ��Ƀg���O�����ɐG���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tongs"&& collision.gameObject.tag != "Meat"&&colorIndex!=3)
        {
             //�Ă����������R���[�`���J�n��
            StartCoroutine(TransferGrilledMeat());

        }

    }
    public IEnumerator TransferGrilledMeat()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Tongs.GetComponent<TongsManager>().GetScoreTextObject(colorIndex, score);// �����Ńg���O�}�l�[�W���[����X�R�A���擾

    }

   
}
