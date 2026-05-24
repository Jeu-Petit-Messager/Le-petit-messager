using UnityEngine;
using Unity.Cinemachine;

public class CameraZoneTrigger : MonoBehaviour
{
    [Header("Configuration de la Caméra")]
    [SerializeField] private CinemachineCamera cameraAActiver;
    [SerializeField] private int prioriteActive = 20;
    [SerializeField] private int proriteInactive = 10; // Corrigé la petite faute de frappe ici au passage !

    [Header("Type de Caméra")]
    [SerializeField] private bool estUneCameraFixe = false;

    [Header("Configuration du Joueur")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est bien le joueur qui entre dans la zone
        if (other.CompareTag(playerTag))
        {
            if (cameraAActiver != null)
            {
                // SÉCURITÉ : On ne force la position QUE si ce n'est PAS une caméra fixe
                if (!estUneCameraFixe)
                {
                    cameraAActiver.ForceCameraPosition(other.transform.position, cameraAActiver.transform.rotation);
                }

                // On monte la priorité de cette caméra pour que le Cinemachine Brain effectue la transition
                cameraAActiver.Priority.Value = prioriteActive;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quand le joueur sort de la zone, on remet la priorité par défaut (inactive)
        if (other.CompareTag(playerTag))
        {
            if (cameraAActiver != null)
            {
                cameraAActiver.Priority.Value = proriteInactive;
            }
        }
    }
}