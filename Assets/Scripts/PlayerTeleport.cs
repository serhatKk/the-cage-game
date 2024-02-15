using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerTeleport : MonoBehaviour
    {
        [SerializeField] private Transform destination;
    

        public Transform GetDestinationn (){
            
            return destination;
            
            }
    }
