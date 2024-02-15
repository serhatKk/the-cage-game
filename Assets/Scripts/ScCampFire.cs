using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCampFire : MonoBehaviour
{

    private GameObject PlayerObj ;
   [SerializeField] private GameObject TextObj; 
    private PlayerControl PlayerDataObj;
    private bool isPlayerEntered = false;
    private bool healState = false;
    //Timer 
    private float timer ; 
    [SerializeField]private float timerMax = 2f;
    [SerializeField]private int healAmount = 10;


    // Start is called before the first frame update
    void Start()
    {
        TextObj.SetActive(false);
        timer = timerMax;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && isPlayerEntered == true)
        {
                if(healState==false) healState = true;
        }
        if (healState==true)
        {
            HealPlayer();
        }
    }
    private void HealPlayer()
    {
        timer = timer- Time.deltaTime;
        if(timer <= 0f)
        {
            PlayerDataObj.getPlayerHealth();
            if( PlayerDataObj.getPlayerHealth() >= 100 )
            {
                healState =false;
            }
             if(healState) PlayerDataObj.TakeDamage(-1*healAmount);
             timer = timerMax;
        }
    }
    
     void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Player")
        {
            TextObj.SetActive(true);
            PlayerObj = other.gameObject;
            PlayerDataObj = PlayerObj.GetComponent<PlayerControl>();
            isPlayerEntered = true ; 
           

        }
    }
     
     void OnTriggerExit2D(Collider2D other) 
     {
        if(other.gameObject.tag == "Player")
        {
            TextObj.SetActive(false);
            isPlayerEntered=false;
             healState = false; 
        }
     }
}
