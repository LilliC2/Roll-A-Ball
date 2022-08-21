using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CameraStyle { Fixed,Free}
public class CameraController : MonoBehaviour
{
    public GameObject player;


    public CameraStyle cameraStyle;
    public Transform pivot;
    public float rotationSpeed = 1f;
    

    //when working with anything to do with positions use a vector3

    private Vector3 offset;
    private Vector3 pivotOffset;

    private bool cameraBool = false;

    void Start()
    {

        
        
        //set the offset to the cameras positon - the players postion. stops camera from  being inside the player
        offset = transform.position - player.transform.position;

        //the offset of the pivot from the player
        pivotOffset = pivot.position - player.transform.position;


        
    }

    private void FixedUpdate()
    {
        //allows player to toggle between modes
        // && is and. || is or
        if (Input.GetKey(KeyCode.C) && (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Plinko")))
        {
            cameraBool = !cameraBool;
        }

         
    }

    


/*there are 3 versions of update
 * Late Update; called after all the other update functions, generally used for cameras
 * Fixed Update; runs indepednt of the frame rate, generally used for physics 
 * Update; runs every frame
 * */
void LateUpdate()
    {
        if (cameraBool == false)
        {
            cameraStyle = CameraStyle.Fixed;
            
        }
        else
        {
            cameraStyle = CameraStyle.Free;
            
        }
        

        //if we are using fixed camera mode
        if (cameraStyle == CameraStyle.Fixed)
        {
            //set the camera position to be the players position + offset
            transform.position = player.transform.position + offset;
        }

        //if we are free camera mode
        if (cameraStyle == CameraStyle.Free)
        {
            //make the pivot positio follow the plauer
            pivot.transform.position = player.transform.position + pivotOffset;

            //work out the angle from the mouse input as a quaternion (which is what is used to deal with angles)
            Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

            //modify offset by the turn angle
            offset = turnAngle * offset;

            //set the camera position to that of the pivot + offset
            transform.position = pivot.transform.position + offset;

            //make the camera look at the pivot
            transform.LookAt(pivot);




        }
                    //OLD METHOD

        /*Set the transform position of the camera to that of the player
        cant just parent it otherwise the camera will rotate with the ball*/
        //transform.position = player.transform.position + offset;
            /*the dot allows you to acces the next part of the command. for example to acces the x value you need to acces position, then acces transform then the variable.
            its like a filing system*/
     


        
    }
}
