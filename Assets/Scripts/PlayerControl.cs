using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : Singleton<PlayerControl>
{
   
    //GameObject
    private GameObject WeaponObj;
    public GameObject Key;
    //component 
    private Rigidbody2D playerRB;

    //Input Variables 
    private float h_Input;
    private float v_Input;
 
    //Player Control Variable
    [SerializeField] private float movementSpeed = 123.0f;
    private float weaponAngle = 0f;
     public float weaponAwayRadius = 1f;

    //equip weaopone
    private bool isWeaponEquipped;
    public bool isKeyEquipped;

    //Animator 
    Animator p_Animator;

    //Charachter States
    private bool isIdle;
    private bool isWalking;
    private bool isWalkingX;
    private bool isWalkingY;
    

    public int maxHealth = 100;
    public int minHealth = 0;
	public int currentHealth;
	public HealthBar healthBar;
    
    [SerializeField] private GameObject CoinCountImage;
    [SerializeField] private GameObject CoinCountTextObj;
    public int coin = 0 ;
    public TextMeshProUGUI CoinCount;
    
    void Awake()
     {
        playerRB = this.GetComponent<Rigidbody2D>();
        p_Animator=this.GetComponent<Animator>();
        isWeaponEquipped=false;
        isKeyEquipped=false;
        isIdle=true;
        isWalking=false;
       
        
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
        CoinCount = CoinCountTextObj.GetComponent<TextMeshProUGUI>();
    

    

    }

    // Update is called once per frame
    void Update()
    {
        CoinCount.text = getCoinCount().ToString();
        CheckInput();
        if(currentHealth <= minHealth)

        {
            Time.timeScale = 0 ; 
            this.gameObject.SetActive(false);
        }
    

    }
    public void setGUIObject(GameObject g1 , GameObject g2)
    {
        CoinCountTextObj = g1 ;
    }



    void FixedUpdate() 
    {
    AppyMovement();
    }

    public int getPlayerHealth ()
    {
        return currentHealth ;
    }
    public int getCoinCount (){
        return coin;
    }
 
 
     public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
    void CheckInput(){
       
        h_Input =Input.GetAxis("Horizontal");
        v_Input = Input.GetAxis("Vertical");

         //Player State Changes
         
        if(h_Input > 0.01f ||  h_Input <-0.01f)   
         {
            isIdle= false;
            isWalkingX=true;
            isWalking=true;
            p_Animator.SetBool("IsWalking",isWalking);
            p_Animator.SetBool("Bool_IsIdle",isIdle);
            p_Animator.SetFloat("MovementX",h_Input);

            
         }
        if(v_Input > 0.01f ||  v_Input < -0.01f)   
         {
            isIdle =false;
            isWalkingY =true ; 
             isWalking=true;
             p_Animator.SetBool("IsWalking",isWalking);
            p_Animator.SetBool("Bool_IsIdle",isIdle);
            p_Animator.SetFloat("MovementY",v_Input);
            
         }
         if(h_Input < 0.01f &&  h_Input >-0.01f) 
         {
            isWalkingX=false;
             
         
         }
         
         if(v_Input < 0.01f &&  v_Input > -0.01f) 
         {
            isWalkingY=false; 
            
         }

        if(isIdle==false && isWalkingX==false && isWalkingY==false)
        {
            isWalking=false;
            isIdle=true;
            p_Animator.SetBool("IsWalking",isWalking);
            p_Animator.SetBool("Bool_IsIdle",isIdle);
             
            
          
           
        }
        
         if (WeaponObj!=null && isWeaponEquipped==true)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - this.transform.position; //WeaponObj.transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            weaponAngle = angle;
           
            WeaponObj.transform.position = new Vector2(this.gameObject.transform.position.x+weaponAwayRadius*Mathf.Cos(angle * Mathf.PI / 180), this.gameObject.transform.position.y + weaponAwayRadius * Mathf.Sin(angle * Mathf.PI / 180));
            WeaponObj.transform.eulerAngles = new Vector3(0,0,angle);
        }
        if (isWeaponEquipped==true && Input.GetButtonDown("Fire1"))
        {
            if (WeaponObj != null)
            {
                WeaponObj.GetComponent<ScHandGun>().shoot(weaponAngle);
            }
            else {Debug.LogError("Error:" + WeaponObj);}
        }
        
  
    
    }
    
    void AppyMovement()
    {
        // input - movementspeed 
        playerRB.velocity = new Vector2(h_Input * movementSpeed,
        v_Input*movementSpeed);
       
    }
    
    void OnTriggerEnter2D(Collider2D col) 
    {
        if(isWeaponEquipped==false && col.CompareTag("Weapon")){
            WeaponObj = col.gameObject;
            col.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x+0.9f,this.gameObject.transform.position.y-0.3f);
            col.gameObject.transform.parent=this.gameObject.transform;
            isWeaponEquipped=true;
        }
       
        
          if(isKeyEquipped==false && col.CompareTag("Key"))
            {
        
            Key = col.gameObject;
            col.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y+2.0f);
            col.gameObject.transform.parent=this.gameObject.transform;
            isKeyEquipped=true;
            
            }
        if (col.CompareTag("Coin")){
            coin +=1;
            Destroy(col.gameObject);
            
        }

        if (col.CompareTag("Water")){
            movementSpeed-=3.0f;
        }
    }  
    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Water")){
            movementSpeed+=3.0f;
        }
    }
    
    
   
    
   
}
