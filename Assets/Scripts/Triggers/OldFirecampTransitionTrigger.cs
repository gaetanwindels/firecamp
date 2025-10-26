using System.Collections;
using Cinemachine;
using DefaultNamespace;
using Rewired;
using UnityEngine;

namespace Triggers
{
    public class OldFirecampTransitionTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject backdrop;
        [SerializeField] private FireCampEvent fireCamEvent;
    
        [SerializeField] private PlayerController playerTrigger;
        [SerializeField] private PlayerController newPlayerToFollow;
        [SerializeField] private Transform fireCampPos1;
        [SerializeField] private Transform fireCampPos2;
        [SerializeField] private Transform fireCampPos3;
        
        [SerializeField] private PlayerController child1;
        [SerializeField] private PlayerController child2;
        [SerializeField] private PlayerController child3;
        
        [SerializeField] private GameObject[] objectsToActivate;
        [SerializeField] private GameObject[] objectsToDeactivate;
        
        [SerializeField] private GameObject cinematic;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player == playerTrigger)
            {
                if (backdrop)
                {
                    backdrop.SetActive(true);
                }
                
                cinematic.SetActive(true);
                player.SetEnabled(false);
                StartCoroutine(ActivateChilds());
                StartCoroutine(ActivateObjects());
            }
        }
    
        IEnumerator ActivateChilds()
        {
            yield return new WaitForSeconds(3f);
            cinematic.SetActive(false);
            PlayerController[] players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);

            foreach (PlayerController player in players)
            {
                player.gameObject.SetActive(false);
            }
            child1.transform.position = fireCampPos2.position;
            child2.transform.position = fireCampPos1.position;
            child3.transform.position = fireCampPos3.position;
            
            child1.SetEnabled(false);
            child2.SetEnabled(false);
            child3.SetEnabled(false);
            child1.gameObject.SetActive(true);
            child2.gameObject.SetActive(true);
            child3.gameObject.SetActive(true);
            //FindFirstObjectByType<CinemachineVirtualCamera>().Follow = newPlayerToFollow.transform;
        }

        IEnumerator ActivateObjects()
        {
            yield return new WaitForSeconds(1f);
        
            foreach (GameObject o in objectsToActivate)
            {
                o.SetActive(true);
            }
        
            foreach (GameObject o in objectsToDeactivate)
            {
                o.SetActive(false);
            }
                
            newPlayerToFollow.SetEnabled(true);
        
            if (backdrop)
            {
                //backdrop.SetActive(false);
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
