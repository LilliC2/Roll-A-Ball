using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltControls : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //assigns rb to object script is attached to
        rb = GetComponent<Rigidbody>();
    }

    // since our player moves with physics we need to rotate our world the same
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical= Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(moveVertical * speed, 0, -moveHorizontal * speed);
        Quaternion deltaRotation = Quaternion.Euler(moveVector * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
