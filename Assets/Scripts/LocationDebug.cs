using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationDebug : MonoBehaviour
{
    // simple debug, testing if the sound is working
    public Vector2 latlon;
    public bool useLocationCoords = false;

    private Vector2 initPos;

    public int numberOfSounds = 4;
    public float sound_distance = 1f;

    public GameObject listener;
    public GameObject soundPrefab;
    
    IEnumerator Start()
    {
        initPos = Tools.GPS2Coords(latlon.x, latlon.y);

        if(useLocationCoords) {
            // only start debugging if the service is running
            while(Input.location.status != LocationServiceStatus.Running){
                yield return new WaitForSeconds(0.5f);
            }
            
            initPos = Tools.GPS2Coords(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
            
        
        listener.transform.position = new Vector3(initPos.x, 0, initPos.y);

        for(int i = 0; i < numberOfSounds; i++) {
            // pick random location around the listener, at the sound distance
            Vector2 soundPos = Random.insideUnitCircle.normalized * sound_distance + initPos;
            GameObject s1 = Instantiate(soundPrefab, new Vector3(soundPos.x, 0, soundPos.y), Quaternion.identity);
            // change the pitch slightly
            s1.GetComponent<AudioSource>().pitch = Random.Range(0.85f,1.0f);
        }
    }
}
