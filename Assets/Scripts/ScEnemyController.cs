using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScEnemyController : MonoBehaviour
{
      
    public GameObject coin ;
    [SerializeField] public GameObject PlayerGo;
    [SerializeField] public Transform PlayerT;
     [SerializeField] public GameObject P_Blood;
    public Vector2 HomeT;
    UnityEngine.AI.NavMeshAgent agent;
    float TargetDist ;
    [SerializeField] private float followRange =8f;



    public int maxHealth = 100;
    public int minHealth = 0;
    public int currentHealth;
    public HealthBar healthBar;
  
  void Awake() 
  {
       
  }

  void Start()
  {
  
    currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    PlayerT = PlayerGo.transform;
    HomeT = new Vector2 (this.transform.position.x , this.transform.position.y); // Başlangıç noktası
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    agent.updateRotation = false;
    agent.updateUpAxis = false;


  }
    void Update() {
        TargetDist = Vector2.Distance(PlayerT.position,this.transform.position);
        if ( TargetDist < followRange){
              agent.SetDestination(PlayerT.position);
        }
        else
        {
             agent.SetDestination(HomeT);
        }
      
    
  }
     void OnCollisionEnter2D(Collision2D other) {
        
       if(other.gameObject.CompareTag("Bullet")){
        
          Destroy(other.gameObject);
          this.gameObject.GetComponent<ScEnemyController>().TakeDamage(other.gameObject.GetComponent<ScHandGunBullet>().getBulletDamage());
        
        if(currentHealth<= minHealth){
          GetComponent<Collider2D>().enabled = false;
          Instantiate(P_Blood, this.transform.position, Quaternion.identity);
          Instantiate(this.coin, this.transform.position, Quaternion.identity);
          Destroy(this.gameObject,0.3f);
        }
          
       }
       if(other.gameObject.CompareTag("Player")) {
             other.gameObject.GetComponent<PlayerControl>().TakeDamage(20);
        
       }
      }


      public void TakeDamage(int damage)
	      {
		      currentHealth -= damage;

		      healthBar.SetHealth(currentHealth);
	      }

}