using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    //variables

    int counter = 0;




    void Start()
    {
        Debug.Log("Hello World");
    }

    void Update()
    {
        //if the user presses the space bar
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //increment counter by 1
            counter += 1; // or counter++; 

            //display counter to player
            Debug.Log("Counter: " + counter);

        }



    }
}
