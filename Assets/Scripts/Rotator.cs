using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    float bobbingSpeed = 2f;
    float height = 0.2f;
    Vector3 pos;

    float rotateSpeed = 2f;

    

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        //rotate our object around an axis over time
        transform.Rotate(new Vector3(0,12,0) * Time.deltaTime * rotateSpeed);

        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * bobbingSpeed) * height + pos.y;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
