using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructObjects : MonoBehaviour
{
	private Rigidbody rb;
	public GameObject destructObject;
    private Vector3 lastVelocity;
    private bool canMove;

   

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody>();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            GameObject instantiated = Instantiate(destructObject, transform.position, transform.rotation);
            instantiated.GetComponent<Destructible>().Destruct(rb.velocity);

        }


    }
}
