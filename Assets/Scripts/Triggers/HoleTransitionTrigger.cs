using System.Collections;
using UnityEngine;

namespace Triggers
{
    public class HoleTransitionTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject backdrop;
        [SerializeField] private GameObject playerFirecampSpawn;
        [SerializeField] private PlayerController playerTrigger;
    
        [SerializeField] private GameObject[] objectsToDeactivate;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player == playerTrigger)
            {
                if (backdrop)
                {
                    backdrop.SetActive(true);
                }
                
                player.SetEnabled(false);
                StartCoroutine(SpawnPlayer(player));
                StartCoroutine(ActivatePlayer(player));
                StartCoroutine(ActivateObjects());
            }
        }

        IEnumerator ActivateObjects()
        {
            yield return new WaitForSeconds(2f);
        
            foreach (GameObject o in objectsToDeactivate)
            {
                o.SetActive(false);
            }
        
            if (backdrop)
            {
                backdrop.SetActive(false);
            }
        }
    
        IEnumerator SpawnPlayer(PlayerController player)
        {
            yield return new WaitForSeconds(1f);
            player.transform.position = playerFirecampSpawn.transform.position;
        }
    
        IEnumerator ActivatePlayer(PlayerController player)
        {
            yield return new WaitForSeconds(2.5f);
            player.SetEnabled(true);
        
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
