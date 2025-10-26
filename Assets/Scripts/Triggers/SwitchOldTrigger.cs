using Cinemachine;
using UnityEngine;

namespace Triggers
{
    public class SwitchOldTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerController playerToTrigger;
        [SerializeField] private PlayerController oldPlayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player == playerToTrigger)
            {
                var virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
                player.IsGettingVeryOld = true;
                player.gameObject.SetActive(false);
                oldPlayer.transform.position = player.transform.position;
                oldPlayer.gameObject.SetActive(true);
                virtualCamera.Follow = oldPlayer.transform;
            }
        }
    
    }
}
