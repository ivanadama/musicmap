using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IPHONE
using UnityEngine.iOS;
#endif

public class LocationService : MonoBehaviour
{
    public Text debugPanel; // debug panel on screen, just an early test
    
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
            yield break;
        }

        #elif UNITY_IPHONE
        if (!Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            debugPanel.text = "Please Turn on Your Location";
            yield break;
        }
        #endif

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser){
            // TODO, add dialog to ask user to turn on location
            debugPanel.text = "Please Turn on Your Location";
            yield break;
        }
            
        // Start service before querying location
        // public void Start(float desiredAccuracyInMeters, float updateDistanceInMeters);
        // defaults are 10,10 ... we could try out 5 or less too!
        Input.location.Start(); 

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
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            debugPanel.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            string currentLocation = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            print(currentLocation);
            debugPanel.text = currentLocation;
        }
    }
}
