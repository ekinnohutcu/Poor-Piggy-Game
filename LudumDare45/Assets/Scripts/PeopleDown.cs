using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleDown : MonoBehaviour
{
    //public Animator anim;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //düşme animasyonu başlatılacak
            
            GetComponent<Collider>().isTrigger = true;
        }
    }
}
