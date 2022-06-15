using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{    
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
         transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - 9.2f);
    }
}
