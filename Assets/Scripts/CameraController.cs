using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * 2f), 
            0, 
            -10
        );
    }
}
