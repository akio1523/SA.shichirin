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

    // ���̐F���Ƃ̃X�R�A��\��float�^�̔z��
    public float[] meatScore = new float[4];

    // ���̐F�̃C���f�b�N�X��\��int�^�̕ϐ�
    private int colorIndex;

    // ���̐F���ς��Ԋu��\��float�^�̕ϐ�
    public float colorChangeTime;

    private float score;

    private float grillMeatSeconds;
    // Start is called before the first frame update
    void Start()
    {
        // FindObjectOfType���\�b�h��GameManager�I�u�W�F�N�g�̃R���|�[�l���g�ւ̎Q�Ƃ��擾����
        //GameManager = FindObjectOfType<GameManager>();

        // colorIndex��0�ɏ���������
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
        //gameObject.GetComponent<Image>().sprite = scorePicture[colorIndex]; �������͕ʂ̃��\�b�h�Ő����H�Ǒł����Q��

    }
}
