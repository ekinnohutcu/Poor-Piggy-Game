using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {  
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [HideInInspector] public Camera cam;
    [HideInInspector] public Transform camParent;
    [HideInInspector] public CamFollow camFollow;
    [HideInInspector] public GameObject player;
    [HideInInspector] public bool isMoving;
    public GameObject startPanel;
    private Quaternion startRot;
    public Transform startTrans;

    public GameObject happyPigStartSound;
    public GameObject coinCollectSound;
    public GameObject rushSound;
    public GameObject deathSound;
    public GameObject mainThemeMusic;
    public GameObject sadViolinEndingMusic;

    private bool doOnceDeath;

    private void Start()
    {
        Application.targetFrameRate = 60;
        cam = Camera.main;
        camParent = cam.transform.parent;
        camFollow = camParent.GetComponent<CamFollow>();
        camFollow.enabled = false;
        player = GameObject.FindWithTag("Player");

        mainThemeMusic.SetActive(true); //!!! PIG FARMA GİRDİĞİMİZDE BUNU KAPATIP YERİNE SAD VIOLIN'I ÇALACAĞIZ. !!!
        happyPigStartSound.SetActive(false);
        deathSound.SetActive(false);
        sadViolinEndingMusic.SetActive(false);
        coinCollectSound.SetActive(false);
    }

    private void Update()
    {
        if (isMoving)
        {
            cam.transform.DOLookAt(player.transform.position,0.1f).SetEase(Ease.Linear);
        }
    }
    public void StartAnimation()
    {
        if (!isMoving)
        {
            startRot = cam.transform.rotation;
            startPanel.SetActive(false);
            isMoving = true;
            transform.DOScale(transform.localScale, 3f).OnComplete(() =>
            {
                player.transform.rotation = startTrans.rotation;
                player.transform.position = startTrans.position;
                player.GetComponent<Animator>().enabled = false;
                cam.transform.rotation = startRot;
                camFollow.enabled = true;
                isMoving = false;
                player.GetComponent<PlayerController>().enabled = true;
                
            });
            player.GetComponent<Animator>().SetTrigger("isStarted");

            happyPigStartSound.SetActive(true);
        }
    }

    public void Quity()
    {
        Application.Quit();
    }

    
    public void Lose()
    {
        //Lose condition
        if (!doOnceDeath)
        {
            transform.DOScale(transform.localScale, 2f).OnComplete(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                });
        }
    }
}
