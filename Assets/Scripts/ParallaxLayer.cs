using System;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    // Config parameters
    [SerializeField] private float layerSpeedX = 10;
    [SerializeField] private float layerSpeedY = 10;
    [SerializeField] private GameObject layerToMove;
    
    private Camera _mainCamera;
    private Vector3 _initialCameraPosition;
    private Vector3 _initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = FindFirstObjectByType<Camera>();
        _initialCameraPosition = _mainCamera.transform.position;
        _initialPosition =  transform.position;
        Debug.Log(_initialPosition);
    }

    private void Awake()
    {
        _initialPosition =  transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!layerToMove)
        {
            return;
        }
        
        var deltaPosition = _initialCameraPosition - _mainCamera.transform.position;

        layerToMove.transform.localPosition = _initialPosition - new Vector3(deltaPosition.x * layerSpeedX, deltaPosition.y * layerSpeedY, 0);
    }
}
