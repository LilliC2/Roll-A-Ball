using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool winCondition = false;

    void Start()
    {
        //gets rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>(); //assigns the rigid body to rb without needing to drag it

        currentSpeed = speed;

        originalRotationValue = transform.rotation; //saves rotation

        //set pickup count to number of gameobjects in the game
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length; //FindGameObjectsWithTag returns an array, so must find length of array
    }

  

    /*fixed update; works different than update, based off actual time between things 
     * rather than frames between things*/

    void FixedUpdate()
    {
        if (winCondition == true)
            return; //function will loop until it reaches return

        //store horizontal axis value in float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //store vertical axis value in float
        float moveVertical = Input.GetAxis("Vertical"); /*not to move up and down like a jump, to move 
                                                        forwards and backwards*/

        //create new vector3 based on horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);/*vector 3 is a coordinate
                                                                            in a 3D plane, x y z*/
        //adds force to our rigid body from our vector times our speed
        rb.AddForce(movement* currentSpeed);

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
    }

    //when player exits collision with boostpad their speed returns to normal
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("BoostPad"))
        {
            currentSpeed = speed;
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        //if we collide with a pickup, destory the pickup

        //checks if collided object has game tag pickup
        if (other.gameObject.CompareTag("Pick Up"))
        {
            //everytime a pickup is destroyed decrease variable
            pickUpCount -= 1;
            //display new pickup count to player
            Debug.Log("Pick Up Count: " + pickUpCount);

            CheckPickUps(); 
            
            Destroy(other.gameObject);
            
        }
    }

    void CheckPickUps()
    {
        //check if the pickup count = 0
        if (pickUpCount == 0)
        {
            //display win message to console 
            Debug.Log("You Win!");
            winCondition = true;
            //set velocity of rb to 0
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }

    }
  


}
