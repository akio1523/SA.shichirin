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
    private bool hasMeat; // �g���O�����̃C���[�W�������Ă��邩�ǂ����̕ϐ�



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

        // �h���b�O�I�����̃}�E�X�|�C���^�[�̈ʒu���擾����
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;

        // ���̈ʒu�ɂ���I�u�W�F�N�g�����o����
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // �I�u�W�F�N�g�����݂��A���ւł����
        if (hit.collider != null && hit.collider.gameObject.tag == "Shichirin")
        {
            // AddMeat���\�b�h���Ăяo��
            Shichirin.GetComponent<ShichirinManager>().AddMeat();
            ImageMeat.SetActive(false);

        }
    }
}
