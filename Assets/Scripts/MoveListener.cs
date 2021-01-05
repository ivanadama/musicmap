using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListener : MonoBehaviour
{
    [SerializeField] bool manualControl = false;
    [SerializeField] float rotSpeed = 3f;
    [SerializeField] float moveSpeed = 5f;

    Vector2 rot = Vector2.zero;
    float lastHeading = 0;
    Vector3 lastPosition = Vector3.zero;


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
            // smoothing out all the visual movements

            Vector3 currentPosition = Listener.getRelativePosition();
            Vector3 smoothPosition = Vector3.Lerp(lastPosition, currentPosition, 0.05f);
            transform.position = smoothPosition;

            lastPosition = smoothPosition;

            // Orient an object to point northward.
            float currentHeading = Input.compass.trueHeading;
            float smoothHeading = Mathf.LerpAngle(lastHeading, currentHeading, 0.05f);
            transform.eulerAngles = new Vector3(0, smoothHeading, 0);

            lastHeading = smoothHeading;
        }
    }
}
