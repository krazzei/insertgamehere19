using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Transform _transform;
    private Vector3 _position;
    
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        GameManager.OnShipDeath += GameManagerOnOnShipDeath;
    }

    private void GameManagerOnOnShipDeath()
    {
        Destroy(this);
    }

    private void Update()
    {
        _position = target.position;
        _position.x = 0;
        _position.z = -10;
        _transform.position = _position;
    }

    private void OnDestroy()
    {
        GameManager.OnShipDeath -= GameManagerOnOnShipDeath;
    }
}