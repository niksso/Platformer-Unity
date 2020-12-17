using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _startPosition;

    [Tooltip("Камера, по которой будет происходить эффект паралакса")] 
    [SerializeField] private Camera camera;
    [Tooltip("Сила эффекта паралакса")]
    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        _startPosition = transform.position.x;
    }

    private void FixedUpdate()
    {
        float distance = camera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
        
    }
}
