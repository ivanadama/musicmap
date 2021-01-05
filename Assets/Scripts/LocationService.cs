using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IPHONE
using UnityEngine.iOS;
#endif

// TODO: Check again for location again after user enables instead of calling fallback

public class LocationService : MonoBehaviour
{
    [SerializeField] Text debugPanel; // debug panel on screen, just an early test
    [SerializeField] float gpsCheckInterval = 0.5f;
    [SerializeField] Vector2 fallbackLocation;
    [SerializeField] UnityEvent OnNewLocation;
    

    IEnumerator Start()
    {
        debugPanel.text = "testing location...";

        #if UNITY_EDITOR
        debugPanel.text = "This is the unity editor";
        // No permission handling needed in Editor
        #elif UNITY_ANDROID

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
            debugPanel.text = "Requesting permissions";
        }

        yield return new WaitForSeconds(5f);
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            debugPanel.text = "Please Turn on Your Location";
            StartFallbackLocation();
            yield break;
        }

        #elif UNITY_IPHONE
        if (!Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            debugPanel.text = "Please Turn on Your Location";
            StartFallbackLocation();
            yield break;
        }
        #endif

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser){
            // TODO, add dialog to ask user to turn on location
            debugPanel.text = "Please Turn on Your Location";
            StartFallbackLocation();
            yield break;
        }

        // Enable Compass
        Input.compass.enabled = true;
            
        // Start service before querying location
        // public void Start(float desiredAccuracyInMeters, float updateDistanceInMeters);
        // defaults are 10,10 ... we could try out 5 or less too!
        Input.location.Start(2,2);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Location Timed out");
            debugPanel.text = "Location Timed Out";

            StartFallbackLocation();

            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            debugPanel.text = "Unable to determine device location";

            StartFallbackLocation();

            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            StartGPSLocation();
        }
    }

    void StartGPSLocation(){
        
        string currentLocation = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
        print(currentLocation);
        debugPanel.text = currentLocation;

        // update Global listener position
        Vector2 coords = Tools.GPS2Coords(Input.location.lastData.latitude, Input.location.lastData.longitude);
        Listener.setPosition(coords);
        OnNewLocation.Invoke();

        // start location update loop
        StartCoroutine(UpdateLocation());
    }

    void StartFallbackLocation(){
        // right now use a fallback
        debugPanel.text = "fallback location";
        Listener.setPosition(fallbackLocation);
        OnNewLocation.Invoke();
    }

    // void Update(){
    //     Vector3 dir = Input.acceleration;  
    //     debugPanel.text = dir.x + "\n" + dir.y + "\n" + dir.z;
    // }

    IEnumerator UpdateLocation(){
        while(true){
            Vector2 gps = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            Vector2 coords = Tools.GPS2Coords(gps.x, gps.y);
            Listener.setPosition(coords);

            Vector3 pos = Listener.getRelativePosition();
            // debugPanel.text = gps.x + " " + gps.y + "\n" + coords.x + " " + coords.y  + "\n" + Input.compass.trueHeading;
            debugPanel.text = pos.x + "\n" + pos.z; //+ "\n" + coords.x + " " + coords.y;
            yield return new WaitForSeconds(gpsCheckInterval);
        }
    }
}
