using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    float bobbingSpeed = 5f;
    float height = 0.2f;
    Vector3 pos;

    float rotateSpeed = 5f;

    

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        //rotate our object around an axis over time
        transform.Rotate(new Vector3(45,12,30) * Time.deltaTime * rotateSpeed);

        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * bobbingSpeed) * height + pos.y;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
