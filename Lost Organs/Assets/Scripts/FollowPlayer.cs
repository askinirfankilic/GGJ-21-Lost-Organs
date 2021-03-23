using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float offsetY;
    private float offsetZ;
    public GameObject player;
    public bool cameraIsShaken = false;
    void Start()
    {
        offsetZ = transform.position.z - player.transform.position.z;
        offsetY = transform.position.y - player.transform.position.y;
    }

    void Update()
    {
        if(cameraIsShaken == false)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);
        }
    }
}
