using System;
using System.Collections;
using System.Collections.Generic;
using Common.CommonScripts;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public static event Action OnPointPicked;
    
    [Header("Rotation Parameter")]
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        RotatePoint();
    }

    private void RotatePoint()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.PlayerTrigger))
        {
            PickPoint();   
        }
    }

    private void PickPoint()
    {
        OnPointPicked?.Invoke();
        
        Destroy(gameObject);
    }
}
