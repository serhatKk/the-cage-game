using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
  
    public GameObject key ;
    
   private void OnCollisionEnter2D(Collision2D other) {
    

       if(other.gameObject.CompareTag("Bullet")){

        Destroy(other.gameObject);
        Instantiate(this.key, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
       
       
        
       }
        
      

    }


}
