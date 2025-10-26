using UnityEngine;

namespace Triggers
{
    public class GettingOldTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerController playerToTrigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player == playerToTrigger)
            {
                player.IsGettingOld = true;
            }
        }
    
    }
}
