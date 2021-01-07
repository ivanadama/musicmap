using UnityEngine;
using System.Collections;

public class Listener : MonoBehaviour
{
    private static Vector3 position = Vector3.zero;
    private static Vector3 initialPosition = Vector3.zero;
    public static void setPosition(Vector2 pos){
        if(initialPosition == Vector3.zero) {
            initialPosition = Tools.vec2ToVec3(pos);
            print("init pos: " + initialPosition);
        }
        position = Tools.vec2ToVec3(pos);
    }
    public static void setPosition(Vector3 pos){
        position = pos;
    }

    public static Vector3 getRelativePosition(){
        return (position - initialPosition) * 1000f; // convert to meters
    }

    public static Vector3 getPosition(){
        return position;
    }
}
