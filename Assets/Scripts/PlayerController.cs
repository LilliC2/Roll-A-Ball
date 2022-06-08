using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb; //rb is common way to write it
    public float speed = 7.0f;//always put f to make it a float

    void Start()
    {
        //gets rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>(); //assigns the rigid body to rb without needing to drag it
    }

    /*fixed update; works different than update, based off actual time between things 
     * rather than frames between things*/

    void FixedUpdate()
    {
        //store horizontal axis value in float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //store vertical axis value in float
        float moveVertical = Input.GetAxis("Vertical"); /*not to move up and down like a jump, to move 
                                                        forwards and backwards*/

        //create new vector3 based on horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);/*vector 3 is a coordinate
                                                                            in a 3D plane, x y z*/
        //adds force to our rigid body from our vector times our speed
        rb.AddForce(movement*speed);

    }
}
