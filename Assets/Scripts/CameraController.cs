using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    //when working with anything to do with positions use a vector3

    private Vector3 offset;

    void Start()
    {
        //set the offset to the cameras positon - the players postion. stops camera from  being inside the player
        offset = transform.position - player.transform.position;
    }


    /*there are 3 versions of update
     * Late Update; called after all the other update functions, generally used for cameras
     * Fixed Update; runs indepednt of the frame rate, generally used for physics 
     * Update; runs every frame
     * */
    void LateUpdate()
    {

        /*Set the transform position of the camera to that of the player
        cant just parent it otherwise the camera will rotate with the ball*/
        transform.position = player.transform.position + offset;
            /*the dot allows you to acces the next part of the command. for example to acces the x value you need to acces position, then acces transform then the variable.
            its like a filing system*/
     
        
    }
}
