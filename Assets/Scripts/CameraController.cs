﻿using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.3f;
    private Vector3 offset;
    private float FOV_MIN = 20f;
    private float FOV_MAX = 80f;
    private readonly float SPEED = 6f;
    private float TURN_SPEED = 4.0f;
    private Transform player;
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset = player.transform.position;
    }

    // Update is called once per frame
    /*void LateUpdate()
    {
        Vector3 desiredPosition = _playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        if(!MoveCameraManually())
            transform.LookAt(_playerTransform);
    }*/
    
   

    void LateUpdate()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        offset = Quaternion.AngleAxis (Input.GetAxis("Horizontal") * TURN_SPEED, Vector3.up) 
                 * Quaternion.AngleAxis (Input.GetAxis("Vertical") * TURN_SPEED, Vector3.right) 
                 * offset;
        transform.position = player.position + offset;
        
        var fieldOfView = camera.fieldOfView;
        fieldOfView = fieldOfView < FOV_MIN ? FOV_MIN :
            fieldOfView > FOV_MAX ? FOV_MAX : fieldOfView - scroll * SPEED;
        camera.fieldOfView = fieldOfView;

        /*if (camera.fieldOfView < FOV_MIN)
        {
            camera.fieldOfView = FOV_MIN;
        }

        if (camera.fieldOfView > FOV_MAX)
        {
            camera.fieldOfView = FOV_MAX;
        }*/
        transform.LookAt(player.position);
    }
    
}
