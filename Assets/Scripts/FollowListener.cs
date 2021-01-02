using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowListener : MonoBehaviour
{
    public GameObject listener;
    public float yOffset = 20f;

    void Update()
    {
        // keep camera above listener
        Vector3 lPos = listener.transform.position;
        transform.position = new Vector3 (lPos.x, yOffset, lPos.z);
    }
}
