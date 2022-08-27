using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{

    Rigidbody rb; //rb is common way to write it
    public float speed = 7.0f;//always put f to make it a float
    private float boostSpeed = 3;
    private float currentSpeed;
    public int pickUpCount; //currently public so we can see it since no UI exists
    private float cannonBoosterThrustVertical = 500;
    private float cannonBoosterThrustHorizontal = 8;
    private float cannonBoosterRotation;
    public float target = 270f;
    public Quaternion originalRotationValue; //quaternion used to represent rotations
    int totalPickUps;
    private bool winCondition = false;
    public Camera camera1;
    public Camera camera2;
    public bool grounded = true;

    public GameObject AxolotlPivot;

    public bool pStartZone;

    public bool canControl = true;




    [Header("UI")]
    public GameObject inGamePanel;

    public TMP_Text scoreText;
    public TMP_Text winText;
    public GameObject winPanel;
    public Image pickUpFill;
    float pickupChunk; //amount we will increment by on fill bar
    private Vector3 offset;

    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    //ctor3 startPos;


    //controllers
    CameraController cameraController;
    GameController gameController;
    Timer timer;
    SoundController soundController;



    private void Start()
    {
        //locks mouse, can't see it on screen
        //Cursor.lockState = CursorLockMode.Locked;

        //turn off win text object
        winPanel.SetActive(false);
        inGamePanel.SetActive(true);

        //gets rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>(); //assigns the rigid body to rb without needing to drag it

        cameraController = FindObjectOfType<CameraController>();

        currentSpeed = speed;

        originalRotationValue = transform.rotation; //saves rotation

        //set pickup count to number of gameobjects in the game
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length; //FindGameObjectsWithTag returns an array, so must find length of array
        //assign amount of pickups to total puickuips
        totalPickUps = pickUpCount;

        //work out the amount of fill for our pick up fill
        pickupChunk = 1.0f / totalPickUps;

        //ensure pickup fill starts at 0
        pickUpFill.fillAmount = 0;

        //display pickups
        CheckPickUps();

        //adds an offset for axolotl so he fits in the ball without clipping out
        offset = AxolotlPivot.transform.position - transform.position;

        //setting up the respawn  point for out of bounds
        resetPoint = GameObject.Find("RespawnPoint");
        originalColour = GetComponent<Renderer>().material.color;
        //ctor3 startPos = transform.position;

        pStartZone = true;


        //Controllers
        gameController = FindObjectOfType<GameController>();
        soundController = FindObjectOfType<SoundController>();
        timer = FindObjectOfType<Timer>();
        //begins countdown if on speedrun mode
        if (gameController.gameType == GameType.Speedrun)
        {
            
            StartCoroutine(PausePlayer());
            
            StartCoroutine(timer.StartCountdown());
            
            



        }
            


    }


    public IEnumerator PausePlayer()
    {
        canControl = false;
        yield return new WaitForSeconds(3f);
        canControl = true;
    }


    /*fixed update; works different than update, based off actual time between things 
     * rather than frames between things*/

    void FixedUpdate()
    {
        
        //moves axolotl to same position as player ball
        AxolotlPivot.transform.position = transform.position + offset;

        

        if (winCondition == true)
            return; //function will loop until it reaches return
        
        if (resetting)
            return;

        if (gameController.controlType == ControlType.WorldTilt)
            return;

        if (canControl == true)
        {
            if (grounded)
            {
                //store horizontal axis value in float
                float moveHorizontal = Input.GetAxis("Horizontal");
                //store vertical axis value in float
                float moveVertical = Input.GetAxis("Vertical"); /*not to move up and down like a jump, to move 
                                                        forwards and backwards*/

                //create new vector3 based on horizontal and vertical values
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);/*vector 3 is a coordinate
                                                                            in a 3D plane, x y z*/

                if (cameraController.cameraStyle == CameraStyle.Free)
                {
                    //rotates the player to the direction of the camera
                    transform.eulerAngles = Camera.main.transform.eulerAngles;

                    //translates the input vectors into cooordinates
                    movement = transform.TransformDirection(movement);
                }

                //adds force to our rigid body from our vector times our speed
                rb.AddForce(movement * currentSpeed);


                Quaternion targetRotation = Quaternion.LookRotation(movement);
                //AxolotlPivot.MoveRotation(targetRotation);

            }
        }
        
           
        



        //&& is and
        if (gameController.gameType == GameType.Speedrun && !timer.IsTiming())
            return;

        
        

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        //when player collides with boostpad their speed increases
        if (collision.gameObject.CompareTag("BoostPad"))
        {
            currentSpeed = speed * boostSpeed; 
        }

        //add force to player when they collide with cannon boost pad
        if (collision.gameObject.CompareTag("CannonBoostPad"))
        {
            soundController.PlayJumpPadSound();
            

            //find the angle of the jump pad
            Vector3 cannonRotation = collision.transform.eulerAngles;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            //get rotation of cannon boost pad
            cannonBoosterRotation = collision.transform.eulerAngles.y;


            //reset rotations of player1
            transform.rotation = Quaternion.identity;
            //rotate player in direction of cannon boost pad
            transform.Rotate(0, cannonBoosterRotation, 0);
            
            //add forward force to object
            rb.velocity = transform.forward* cannonBoosterThrustHorizontal;

            //add vertical force to object
            rb.AddForce(0, cannonBoosterThrustVertical, 0);
            

        }

        //to respawn player at start
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer()); //calls on function
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;

        if(collision.gameObject.CompareTag("Wall"))
        {
            soundController.PlayCollisionSound(collision.gameObject);
        }
    }

    //when player exits collision with boostpad their speed returns to normal
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("BoostPad"))
        {
            currentSpeed = speed;
            soundController.PlayBoostPadSound();
        }
        
        if (collision.collider.CompareTag("Ground"))
            grounded = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        //if we collide with a pickup, destory the pickup

        //checks if collided object has game tag pickup
        if (other.gameObject.CompareTag("Pick Up"))
        {
            //everytime a pickup is destroyed decrease variable
            pickUpCount -= 1;
            //increase fill amount of our pick up fill image
            pickUpFill.fillAmount = pickUpFill.fillAmount + pickupChunk;
            soundController.PlayPickupSound();
            CheckPickUps();
            Destroy(other.gameObject);


        }

        if (other.gameObject.CompareTag("PlinkoStartZone"))
        {

            pStartZone = true;
            camera2.gameObject.SetActive(false);
            camera1.gameObject.SetActive(true);


        }
        

        if (other.gameObject.CompareTag("Finish")) 
        {
            //can make module for winning but do that later

            //display win message to player 
            winPanel.SetActive(true);
            winCondition = true;
            //set velocity of rb to 0
            //rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlinkoStartZone"))
        {

            pStartZone = false;
            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(true);


        }
    }


    void CheckPickUps()
    {
        //display pickupcount
        scoreText.text = "Pick ups left: " + pickUpCount.ToString() + "/" + totalPickUps.ToString();

        if (pickUpCount == 0)
            WinGame();




    }
  
    void WinGame()
    {

        soundController.PlayWinSound();
        //display win message to player
        winPanel.SetActive(true);
        inGamePanel.SetActive(false);
        winCondition = true;
        //set velocity of rb to 0
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (gameController.gameType == GameType.Speedrun)
            timer.StopTimer();
        


    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;

        }

        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;

    }


    // temp reset functionality
    public void ResetGame() 
    {
        

        //load the active scene again
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
        

}
