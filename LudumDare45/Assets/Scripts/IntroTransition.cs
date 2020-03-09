using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTransition : MonoBehaviour
{
	int currentImage = 0;

	public GameObject[] Images;

    private int i; // weri important
    void Update()
    {
        if(currentImage == 4)
		{
            SceneManager.LoadScene("Game");
        }
        for (int i = 0; i < 4; i++)
        {
            if(i == currentImage)
            {
                Images[i].SetActive(true);
            }
            else
            {
                Images[i].SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            currentImage++;
        }
    }
}
