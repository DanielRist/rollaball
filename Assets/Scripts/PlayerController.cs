using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI instructionText;
    public GameObject playerBall;

    private Rigidbody rb;
    private int count;
    private bool finish;
    private float movementX;
    private float movementY;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        timeElapsed = 0;
        finish = false;

        SetCountText();
        winTextObject.SetActive(finish);
        instructionText.CrossFadeAlpha(0.0f, 6.0f, false);
        Cursor.visible = false;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        SetTimeText();
    }
   void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }





    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("FinishLine"))
        {
            if(count >= 12) 
            {
                finish = true;
                SetCountText();
            }
            SetCountText();
        }        
        
    }



    

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12 && finish)
        {
           winTextObject.SetActive(true);
        }
    }

    void SetTimeText()
    {
        if(!finish || count < 12)
        {
            float roundTime = (Mathf.Round(timeElapsed * 100)/100);
            timeText.text = "Time: " + roundTime.ToString();
        } else {
            playerBall.SetActive(false);
            Cursor.visible = true; 
            Invoke("LoadLevelGame", 5.0f);
        }

    }

    void LoadLevelGame()
    {
        SceneManager.LoadScene(2);
    }



}
