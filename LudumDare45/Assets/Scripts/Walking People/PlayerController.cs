using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float chargeSpeed;
    private float normalSpeed;
    public float rotationSpeed;

    private bool jumpEnabled = true;

    private bool canMove;

    private Rigidbody rb;

    public GameObject destructObject;
    public Vector3 lastVelocity;

    public GameObject runParticle;
    public GameObject runParticlePos;

    public int coinCount;

    public bool canRush;
    public bool isOverMinRushCount;

    public bool isRushing;
    
    [FormerlySerializedAs("maxRushTime")] public float rushCount;
    public float _currentRushCount;

    public Image rushPowerImage;

    public GameManager gameManagerScript;

    public TextMeshProUGUI coinText;

    private AudioSource rushAud;

    // Start is called before the first frame update
    void Start()
    {
        rushAud = GameManager.Instance.rushSound.GetComponent<AudioSource>();
        normalSpeed = speed;
        chargeSpeed = speed * 3f;
        _currentRushCount = rushCount;
        canMove = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector3(transform.forward.x * speed, rb.velocity.y, transform.forward.z * speed);
            InputController();
        }


        if (_currentRushCount < rushCount && !isRushing)
        {
            _currentRushCount += Time.deltaTime;
        }
        if (_currentRushCount > rushCount && isOverMinRushCount)
        {
            canRush = true;
        }
        if (_currentRushCount <= 0)
        {
            canRush = false;
        }
        if (_currentRushCount < rushCount/10)
        {
            isOverMinRushCount = false;
        }
        else
        {
            isOverMinRushCount = true;
            canRush = true;
        }

        rushPowerImage.fillAmount = _currentRushCount / rushCount;

    }

    void InputController()
    {
        if(Input.GetKey(KeyCode.A))
        {
            //Turn Left

            transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Turn Right

            transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
        }
        
        if (rb.velocity.magnitude > 0.2f)
        {
            lastVelocity = rb.velocity;
        }

        if (canRush)
        {
            if (Input.GetKey(KeyCode.W))
            {
                DoRush();
                if (!rushAud.isPlaying)
                {
                    rushAud.Play();
                }
            }
            else
            {
                isRushing = false;
                if (speed != normalSpeed)
                {
                    speed = normalSpeed;
                }
                if (rushAud.isPlaying)
                {
                    rushAud.Stop();
                }
            }
        }
        else
        {
            isRushing = false;
            if (speed != normalSpeed)
            {
                speed = normalSpeed;
            }
            if (rushAud.isPlaying)
            {
                rushAud.Stop();
            }
        }
    }

    private void DoRush()
    {
        Instantiate(runParticle, runParticlePos.transform.position, runParticlePos.transform.rotation);
        isRushing = true;
        speed = chargeSpeed;
        _currentRushCount -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall")  || collision.transform.CompareTag("people"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.camFollow.enabled = false;
            GameObject instantiated = Instantiate(destructObject, transform.position, transform.rotation);
            instantiated.GetComponent<Destructible>().Destruct(lastVelocity);
            canMove = false;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            GetComponent<Collider>().isTrigger = true;

            PlayDeathSound();
            GameManager.Instance.Lose();

            gameManagerScript.rushSound.SetActive(false);
        }
        if (collision.transform.CompareTag("furniture") && !isRushing)
        {
            gameObject.SetActive(false);
            GameManager.Instance.camFollow.enabled = false;
            GameObject instantiated = Instantiate(destructObject, transform.position, transform.rotation);
            instantiated.GetComponent<Destructible>().Destruct(lastVelocity);
            canMove = false;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
            PlayDeathSound();
            gameManagerScript.rushSound.SetActive(false);
            GameManager.Instance.Lose();
        }

        if(collision.transform.CompareTag("coin"))
        {
            coinCount++;
            coinText.text = "COINS " + coinCount;
            Debug.Log("Coin count is: " + coinCount);
            Destroy(collision.rigidbody.gameObject);
            gameManagerScript.coinCollectSound.SetActive(false);
            gameManagerScript.coinCollectSound.SetActive(true);
        }

        if (collision.transform.CompareTag("barn") && coinCount >=1)
        {
            Debug.Log("çarptım");
            SceneManager.LoadScene("Outro");
        }

        

    }

    private void PlayDeathSound()
    {
        gameManagerScript.deathSound.SetActive(false);
        gameManagerScript.deathSound.SetActive(true);
    }
}