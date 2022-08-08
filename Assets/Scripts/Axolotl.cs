using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axolotl : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
        

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation.normalized,
            360 * Time.fixedDeltaTime); //add time detla to slow it down otherwise it would update every frame

            //rotates axoltl based on horixonal and vertial input, which is same input player has to move with
        rb.MoveRotation(targetRotation);
    }
}
