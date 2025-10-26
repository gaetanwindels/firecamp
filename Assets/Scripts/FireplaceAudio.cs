using UnityEngine;

public class FireplaceAudio : MonoBehaviour
{
    [SerializeField] private AudioSource fireplaceAudio;
    [SerializeField] private float cameraDistanceY = 2;
    [SerializeField] private float cameraDistanceX = 2;
    
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = FindFirstObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_mainCamera.transform.position.y - fireplaceAudio.gameObject.transform.position.y) <= cameraDistanceY &&
            Mathf.Abs(_mainCamera.transform.position.x - fireplaceAudio.gameObject.transform.position.x) <= cameraDistanceX)
        {
            if (!fireplaceAudio.isPlaying)
            {
                fireplaceAudio.Play();
            }
            
        }
        else
        {
            fireplaceAudio.Stop();
        }
    }
}
