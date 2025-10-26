using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class FirecampTransitionTrigger1 : MonoBehaviour
{
    [SerializeField] private GameObject backdrop;
    
    [SerializeField] private PlayerController newPlayerToFollow;
    [SerializeField] private PlayerController playerTrigger;
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private GameObject[] objectsToDeactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (backdrop)
        {
            backdrop.SetActive(true);
        }

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player == playerTrigger)
        {
            player.SetEnabled(false);
            StartCoroutine(FollowPlayer());
            StartCoroutine(ActivateObjects());
        }
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
            Debug.Log("ACTIVATE OBJECT");
            o.SetActive(true);
        }
        
        foreach (GameObject o in objectsToDeactivate)
        {
            o.SetActive(false);
        }
                
        newPlayerToFollow.SetEnabled(true);
        
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
