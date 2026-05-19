using UnityEngine;
using Unity.Cinemachine;

public class CameraZoneTrigger : MonoBehaviour
{
    [Header("Configuration de la Caméra")]
    [SerializeField] private CinemachineCamera cameraToActivate;
    [SerializeField] private int activePriority = 20;
    [SerializeField] private int inactivePriority = 10;

    [Header("Configuration du Joueur")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est bien le joueur qui entre dans la zone
        if (other.CompareTag(playerTag))
        {
            if (cameraToActivate != null)
            {
                cameraToActivate.ForceCameraPosition(other.transform.position, cameraToActivate.transform.rotation);
                // On monte la priorité de cette caméra pour que le Cinemachine Brain effectue la transition
                cameraToActivate.Priority.Value = activePriority;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optionnel : Quand le joueur sort de la zone, on peut baisser la priorité
        if (other.CompareTag(playerTag))
        {
            if (cameraToActivate != null)
            {
                cameraToActivate.Priority.Value = inactivePriority;
            }
        }
    }
}