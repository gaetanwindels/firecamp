using UnityEngine;

namespace Triggers
{
    public class GettingVeryOldTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerController playerToTrigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player == playerToTrigger)
            {
                player.IsGettingVeryOld = true;
                
            }
        }
    
    }
}
