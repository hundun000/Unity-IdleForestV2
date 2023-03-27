using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointFollow : MonoBehaviour
{
    public Camera cameraToFollow;

    void Update()
    {
        transform.position = new(cameraToFollow.transform.position.x, cameraToFollow.transform.position.y, 0);
    }
}
