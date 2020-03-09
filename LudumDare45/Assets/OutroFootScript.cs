using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroFootScript : MonoBehaviour
{

    public AudioManager audmanager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audmanager.PlayCrash();
            other.GetComponent<Destructible>().Destruct(Vector3.down);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
