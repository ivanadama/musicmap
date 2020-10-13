using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif


public class LocationService : MonoBehaviour
{
    public Text debugPanel;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        debugPanel.text = "testing location...";

        #if UNITY_EDITOR
        debugPanel.text = "This is the unity editor";
        // No permission handling needed in Editor
        #elif PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)) {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            debugPanel.text = "Requesting permissions";
        }

        yield return new WaitForSeconds(2f);
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            debugPanel.text = "Android and Location not enabled";
            yield break;
        }

        #elif UNITY_IOS
        if (!Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            debugPanel.text = "IOS and Location not enabled";
            yield break;
        }
        #endif

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser){
            debugPanel.text = "Service not enabled";
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
            print("Timed out");
            debugPanel.text = "Timed Out";
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

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
