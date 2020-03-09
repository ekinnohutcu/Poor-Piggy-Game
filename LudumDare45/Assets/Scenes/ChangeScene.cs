using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player")  && collision.transform.GetComponent<PlayerController>().coinCount >=10) {

            SceneManager.LoadScene("Outro");
        }
    }
}
