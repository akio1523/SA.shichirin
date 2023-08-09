using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TongsMovement : MonoBehaviour,IDragHandler

{
    public void OnDrag(PointerEventData eventData) 
    {
        Vector3 tongsPos = Camera.main.ScreenToWorldPoint(eventData.position);
        tongsPos.z = 0;
        transform.position = tongsPos;
    }
}
