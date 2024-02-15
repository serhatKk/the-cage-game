using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class CanvasControl : Singleton<CanvasControl>
{
    [SerializeField] private GameObject InGameGUIObj;
    [SerializeField] private GameObject InGameMenuObj;
    [SerializeField] private GameObject PlayerGO;
    [SerializeField] private PlayerControl PlayerScript;
    
    int sceneID;

    private bool isActive_InGameMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        InGameGUIObj = this.transform.GetChild(0).gameObject;
        InGameMenuObj = this.transform.GetChild(1).gameObject;
        isActive_InGameMenu = false;
        InGameMenuObj.SetActive(isActive_InGameMenu);
         
         PlayerGO = GameObject.FindWithTag("Player");
         if(PlayerGO != null)
         {
            PlayerScript = PlayerGO.GetComponent<PlayerControl>();
         }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P))
        {
          InGameMenu_Continue();
        }
    }
    public void InGameMenu_Continue()
    {
        //InGame Toogle
        if(isActive_InGameMenu==false)
        {
            isActive_InGameMenu = true;
           //InGameGUIObj.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            isActive_InGameMenu=false;
             Time.timeScale = 1;
        }
       
        InGameGUIObj.SetActive(isActive_InGameMenu); 

    }

         public void InGameMenu_EXit()
         {
            SceneManager.LoadScene(sceneID-1);
         }

         public void setPlayer(GameObject PlayerObject)
         {
              PlayerGO   =  PlayerObject ;
         }
    

        public void setPlayerGUIObjects(){
            if(PlayerGO != null)
            {
                 if (InGameGUIObj == null ||InGameMenuObj == null )
                {
                InGameGUIObj = this.transform.GetChild(0).gameObject;
                InGameMenuObj = this.transform.GetChild(1).gameObject;
                 }
              PlayerScript = PlayerGO.GetComponent<PlayerControl>();
              GameObject CoinCountTextObj =InGameGUIObj.transform.GetChild(0).gameObject ;
              GameObject CoinCountImage =InGameGUIObj.transform.GetChild(1).gameObject ;
              PlayerScript.setGUIObject(CoinCountTextObj,CoinCountImage);

            }
            else 
            Debug.Log("as");
        }
}
