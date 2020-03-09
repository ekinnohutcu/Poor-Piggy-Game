using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithPlayer : MonoBehaviour
{
    public PlayerController playerControllerScript;
    public GameObject destructObject;
    public GameObject Coins;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Instantiate(Coins, transform.position, transform.rotation);
            gameObject.SetActive(false);
            Vector3 lastVelocity = playerControllerScript.lastVelocity;
            GameObject instantiated = Instantiate(destructObject, transform.position, transform.rotation);
            instantiated.GetComponent<Destructible>().Destruct(lastVelocity);
            GetComponent<Collider>().isTrigger = true;
        }


    }
}
