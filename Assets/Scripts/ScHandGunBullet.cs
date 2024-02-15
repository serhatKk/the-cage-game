using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScHandGunBullet : MonoBehaviour
{
    [SerializeField] public float bulletSpeed;
    [SerializeField] public int bulletDamage = 50;
    [SerializeField] float lifeTime = 4f;
      
  



    public void shootBullet(float angle)
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*Mathf.Cos(angle*Mathf.PI/180), bulletSpeed * Mathf.Sin(angle * Mathf.PI / 180));

       Destroy(this.gameObject,lifeTime);
    }

   private void OnCollisionEnter2D(Collision2D other) {
    

        if (other.gameObject.CompareTag("Obstacle"))
        {
            BoxCollider2D m_ObjectCollider = this.gameObject.GetComponent <BoxCollider2D>(); m_ObjectCollider.isTrigger = false;
            Destroy(this.gameObject);
        }
       
    }
    public int getBulletDamage(){
        return bulletDamage;
    }
}