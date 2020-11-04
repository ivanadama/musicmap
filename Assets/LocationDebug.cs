using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationDebug : MonoBehaviour
{
    public Vector2 latlon;
    public Vector2 soundLatLon = new Vector2(38.311080f, 122.274121f);
    public Vector2 soundLatLon2 = new Vector2(38.309011f, 122.273930f);
    private Vector2 initPos;

    public GameObject listener;
    public GameObject soundPrefab;
    // Start is called before the first frame update
    void Start()
    {
        initPos = Tools.GPS2Coords(latlon.x, latlon.y);
        listener.transform.position = new Vector3(initPos.x, 0, initPos.y);

        Vector2 soundPos = Tools.GPS2Coords(soundLatLon.x, soundLatLon.y);
        GameObject s1 =  Instantiate(soundPrefab,new Vector3(soundPos.x,0,soundPos.y), Quaternion.identity);
        s1.GetComponent<AudioSource>().pitch = Random.Range(0.95f,1.05f);

        Vector2 soundPos2 = Tools.GPS2Coords(soundLatLon2.x, soundLatLon2.y);
        GameObject s2 = Instantiate(soundPrefab,new Vector3(soundPos2.x,0,soundPos2.y), Quaternion.identity);
        s2.GetComponent<AudioSource>().pitch = Random.Range(0.95f,1.05f);

        print(initPos);
        print(soundPos);
        print(Vector2.Distance(initPos,soundPos));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
