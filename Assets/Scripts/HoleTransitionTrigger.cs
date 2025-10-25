using System;
using System.Collections;
using UnityEngine;

public class HoleTransitionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject backdrop;
    [SerializeField] private GameObject firstPlayerFirecampSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (backdrop)
        {
            backdrop.SetActive(true);
        }

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.SetEnabled(false);
            StartCoroutine(SpawnPlayer(player));
            StartCoroutine(ActivatePlayer(player));
        }
    }

    IEnumerator SpawnPlayer(PlayerController player)
    {
        yield return new WaitForSeconds(1f);
        player.transform.position = firstPlayerFirecampSpawn.transform.position;
    }
    
    IEnumerator ActivatePlayer(PlayerController player)
    {
        yield return new WaitForSeconds(2.5f);
        player.SetEnabled(true);
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
