using System.Collections;
using Cinemachine;
using DefaultNamespace;
using Unity.Properties;
using UnityEngine;

namespace Triggers
{
    public class FirecampTransitionTrigger1 : MonoBehaviour
    {
        [SerializeField] private GameObject backdrop;
        [SerializeField] private FireCampEvent fireCamEvent;
        [SerializeField] private GameObject cinematic;
        
        [SerializeField] private float transitionDelay = 2f;
    
        [SerializeField] private PlayerController newPlayerToFollow;
        [SerializeField] private PlayerController playerTrigger;
        [SerializeField] private GameObject[] objectsToActivate;
        [SerializeField] private GameObject[] objectsToDeactivate;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player == playerTrigger)
            {
                cinematic.SetActive(true);
                player.SetEnabled(false);
                StartCoroutine(LaunchTransition());
            }
        }
        
        IEnumerator LaunchTransition()
        {
            yield return new WaitForSeconds(transitionDelay);
                
            if (backdrop)
            {
                backdrop.SetActive(true);
            }
                
            StartCoroutine(FollowPlayer());
            StartCoroutine(ActivateObjects());
        }
    
        IEnumerator FollowPlayer()
        {
            yield return new WaitForSeconds(1f);
            FindFirstObjectByType<CinemachineVirtualCamera>().Follow = newPlayerToFollow.transform;
        }

        IEnumerator ActivateObjects()
        {
            yield return new WaitForSeconds(2f);
        
            foreach (GameObject o in objectsToActivate)
            {
                o.SetActive(true);
            }
        
            foreach (GameObject o in objectsToDeactivate)
            {
                o.SetActive(false);
            }
                
            newPlayerToFollow.SetEnabled(true);
            cinematic.SetActive(false);
            
            if (backdrop)
            {
                backdrop.SetActive(false);
            }
        }
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
