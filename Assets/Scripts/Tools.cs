using UnityEngine;

public static class Tools
{
    // radius of the Earth, changing to km to avoid floating point problems
    private static float radius = 6378.100f; 
    private static float centerPoint = -1f;

    // Equirectangular Projection using the initial position of the device as the center of the map
    public static Vector2 GPS2Coords(float latitude, float longitude) {
        // convert to radians
        latitude = Mathf.Deg2Rad * latitude;
        longitude = Mathf.Deg2Rad * longitude;

        // choose the center of our map first time
        if(centerPoint == -1f) {
            centerPoint = Mathf.Cos(latitude);
        }

        // calculate x,y
        float x = radius*longitude*centerPoint;
        float y = radius*latitude;

        return new Vector2(x,y);
    }

    // different version test
    // float x =  ((100000f/360.0f) * (180f + longitude));
    // float y =  ((100000f/180.0f) * (90f - latitude));

}
