using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TongsManager : MonoBehaviour,IDragHandler
    
{
    public GameObject ImageMeat;
    public Sprite rawMeatPicture ;
    public void OnDrag(PointerEventData eventData) 
    {
        Vector3 tongsPos = Camera.main.ScreenToWorldPoint(eventData.position);
        tongsPos.z = 0;
        transform.position = tongsPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Plate")
        {
            ImageMeat.GetComponent<UnityEngine.UI.Image>().sprite = rawMeatPicture;
        }
    }
}
