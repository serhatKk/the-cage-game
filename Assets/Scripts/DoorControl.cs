using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class DoorControl : MonoBehaviour
{
    GameObject GameManager;
    [SerializeField] public PlayerControl player;
    [SerializeField] private TextMeshProUGUI TextObj;     
    //Scene sceneID;

     

    // Start is called before the first frame update
    void Start()
    {
         //  sceneID = SceneManager.GetActiveScene();
           TextObj.enabled = true;
           TextObj.text = "YOU NEED TO FIND KEY";
           GameManager=GameObject.FindWithTag("GM");
         
    }
private void OnTriggerEnter2D(Collider2D other) {
       
       
       if (other.CompareTag("Player") && player.isKeyEquipped==false){
             TextObj.text = "YOU NEED TO FIND KEY";          
             TextObj.enabled = true;
            
       }
  
       else if(other.CompareTag("Player") && player.isKeyEquipped==true){
            TextObj.text = "GOOD JOB";
            player.isKeyEquipped = false;
            TextObj.enabled = true;
            Destroy(player.Key);
            DontDestroyOnLoad(other.gameObject);            
            //SceneManager.LoadScene(sceneID.buildIndex+1);
            GameManager.GetComponent<ScGameManager>().LoadNextScene();
           

           
       }
       
    }
     void OnTriggerExit2D(Collider2D other) {
              TextObj.enabled = false;
    }

     
}
