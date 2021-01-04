using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationDebug : MonoBehaviour
{
    // simple debug, testing if the sound is working
    public Vector2 latlon;
    public bool useGPSCoords = false;
    public GameObject listenerObject;

    private Vector2 initPos;


    IEnumerator Start()
    {
        initPos = Tools.GPS2Coords(latlon.x, latlon.y);

        if(useGPSCoords) {
            // only start debugging if the service is running
            while(Input.location.status != LocationServiceStatus.Running){
                yield return new WaitForSeconds(0.5f);
            }
            
            initPos = Tools.GPS2Coords(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
            
        // update Listener's position
        Listener.setPosition(initPos);

        // move the listener object
        listenerObject.transform.position = Listener.getPosition();
    }
}
