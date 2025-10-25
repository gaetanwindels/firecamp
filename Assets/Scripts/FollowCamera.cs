using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = FindFirstObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =  new Vector3(_mainCamera.transform.position.x, transform.position.y, transform.position.z);
    }
}
