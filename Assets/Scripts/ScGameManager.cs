using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScGameManager : Singleton<ScGameManager>
{
    [SerializeField] private GameObject PlayerPrefab;
    private GameObject PlayerGO ;
    private PlayerControl PlayerDMSc;

    //------------------------------------

    [SerializeField] private GameObject IngameCanvas_Prefab;
    private GameObject INGameGUI;
    private GameObject InGameCanvas;
    private CanvasControl InGameCanvasScript;

    //Scene Bilgi
    Scene currentScene;



    // Start is called before the first frame update
    void Start()
    {
        //Get Current Scene 
          currentScene = SceneManager.GetActiveScene();
     
          //Instantiate Player 
           PlayerGO = Instantiate(PlayerPrefab,new Vector3(0,0,0),Quaternion.identity);
           PlayerDMSc = PlayerGO.GetComponent<PlayerControl>();

          //Instantiate Canvas 
          //INGameGUI = Instantiate(GUI_Ingame_Prefab,new Vector3(0,0,0),Quaternion.identity);
          //IngameCanvas = INGameGUI.transform.GetChild(0).gameObject;
          InGameCanvas = Instantiate(IngameCanvas_Prefab,new Vector3(0,0,0),Quaternion.identity);
          InGameCanvasScript = InGameCanvas.GetComponent<CanvasControl>();
          
          //InGameCanvasScript.setPlayer(PlayerGO);
          //InGameCanvasScript.setPlayerGUIObjects();
        //  InGameCanvasScript.setGUIObject()
        if (PlayerGO != null)
        {
            Debug.Log("PlayerGO geldi");

            if (InGameCanvas != null)
            {
                Debug.Log("InGameCanvas geldi");
                
                if (InGameCanvasScript != null)
                {
                    Debug.Log("InGameCanvasScript geldi");
                    
                    InGameCanvasScript.setPlayer(PlayerGO);
                    InGameCanvasScript.setPlayerGUIObjects();
                } else{
                    Debug.Log("InGameCanvasScript gelmedi");
                }
            } else{
                Debug.Log("InGameCanvas gelmedi");
            }
            
        } else{
            Debug.Log("PlayerGO gelmedi");
        }
        
        if (currentScene.buildIndex == 0)
        {
            //PlayerGO.SetActive(false);
            InGameCanvas.SetActive(false);
        } 
          
    }   


    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene(){

        currentScene = SceneManager.GetActiveScene();
    
        //Dosyadan sahne numarasına göre baş poz. elde etme 
      
        SceneManager.LoadScene(currentScene.buildIndex+1);
        
    }
}
