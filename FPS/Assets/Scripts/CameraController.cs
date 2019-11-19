using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player_transform;
    private Transform camera_transform;
    private Vector3 offset;
    void Start()
    {
        camera_transform = GetComponent<Transform>();
        offset = camera_transform.position - player_transform.position;
        Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {
        camera_transform.position = player_transform.position + offset;
    }
}
