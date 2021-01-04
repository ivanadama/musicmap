using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListener : MonoBehaviour
{
    [SerializeField] bool manualControl = false;
    [SerializeField] float rotSpeed = 3f;
    [SerializeField] float moveSpeed = 5f;

    Vector2 rot = Vector2.zero;


    void Update(){
        if(manualControl){
            // rotate with mouse
            rot.y += Input.GetAxis ("Mouse X");
            transform.eulerAngles = (Vector2)rot * rotSpeed;
            // tank controls
            transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            // update global listener
            Listener.setPosition(transform.position);
        } else {
            transform.position = Listener.getPosition();

            // Orient an object to point northward.
            transform.eulerAngles = new Vector3(0, Input.compass.trueHeading, 0);
        }
    }
}
