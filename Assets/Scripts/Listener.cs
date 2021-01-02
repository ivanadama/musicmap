using UnityEngine;
using System.Collections;

public class Listener : MonoBehaviour
{
    private static Vector3 position = Vector3.zero;
    public static void setPosition(Vector2 pos){
        position = Tools.vec2ToVec3(pos);
    }
    public static void setPosition(Vector3 pos){
        position = pos;
    }

    public static Vector3 getPosition(){
        return position;
    }
}
