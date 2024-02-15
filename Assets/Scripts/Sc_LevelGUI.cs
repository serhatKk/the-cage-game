using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_LevelGUI : MonoBehaviour
{
   int sceneID;
  
    // Start is called before the first frame update
    void Start()
    {
          sceneID = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fucn_ExitGame(){
        Application.Quit();
      

    }

    public void fucn_StartNewGame()
    {
        SceneManager.LoadScene(sceneID+1);

    }
    
    public void fucn_LoadGame()
    {

    }

    public void fucn_Settings(){

        }

}
