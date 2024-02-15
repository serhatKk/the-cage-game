using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCanvas : MonoBehaviour
{
    [SerializeField] private GameObject InGameGUIObj;
    [SerializeField] private GameObject InGameMenuObj;

    private bool isActive_InGameMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        InGameGUIObj = this.transform.GetChild(1).gameObject;
        InGameMenuObj = this.transform.GetChild(1).gameObject;
        isActive_InGameMenu = false;
        InGameMenuObj.SetActive(isActive_InGameMenu);
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
        if(isActive_InGameMenu==false){
            isActive_InGameMenu = true;
            Time.timeScale = 0;
        }
        else {
            isActive_InGameMenu=false;
             Time.timeScale = 1;
        }
        InGameGUIObj.SetActive(isActive_InGameMenu); 

    }
}
