using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShichirinManager : MonoBehaviour
{
    public GameObject meatPrefab;
    public GameObject GameManager;
    GameObject meat;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddMeat()
    {
      meat = Instantiate(meatPrefab, transform);
            //meat.transform.SetParent(gameObject.transform, false);
            meat.transform.position = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
      }

    public void TouchMeat()
    {
        GameManager.GetComponent<GameManager>().RefreshScoreText(2.5f);
        Destroy(meat);
    }
}
