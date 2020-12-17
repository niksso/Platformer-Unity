using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Объект, за которым слеует камера.")]
    [SerializeField] private Transform target;
    [Tooltip("Плавность следования камеры.")]
    [SerializeField] private float smooth= 5.0f;
    [Tooltip("Отклонение от объекта.")]
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -5);
    
    void  FixedUpdate (){
        transform.position = Vector3.Lerp (transform.position, target.position + offset, Time.deltaTime * smooth);
    } 
}
