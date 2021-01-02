using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListener : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 rot = Vector2.zero;
    Vector3 pos; 
	public float rotSpeed = 3f;
    public float moveSpeed = 5f;

    void Start(){
        pos = Listener.getPosition(); 
    }

    void Update(){
        rot.y += Input.GetAxis ("Mouse X");
		transform.eulerAngles = (Vector2)rot * rotSpeed;

        // tank controls
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // if(pos == Vector3.zero){
        //     pos = Listener.position;
        // } else {
        //     // raw position move
        //     pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //     pos.y += Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; 
        // }
        // transform.position = new Vector3(pos.x, 0, pos.y);

        Listener.setPosition(transform.position);
    }
}
