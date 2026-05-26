using UnityEngine;
using Unity.Cinemachine;

public class GestionCamera : MonoBehaviour
{
    [Header("Configuration de la Cam�ra")]
    [SerializeField] private CinemachineCamera cameraAActiver;
    [SerializeField] private int prioriteActive = 20;
    [SerializeField] private int proriteInactive = 10; // Corrig� la petite faute de frappe ici au passage !

    [Header("Type de Cam�ra")]
    [SerializeField] private bool estUneCameraFixe = false;

    [Header("Configuration du Joueur")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // On v�rifie si c'est bien le joueur qui entre dans la zone
        if (other.CompareTag(playerTag))
        {
            if (cameraAActiver != null)
            {
                // S�CURIT� : On ne force la position QUE si ce n'est PAS une cam�ra fixe
                if (!estUneCameraFixe)
                {
                    cameraAActiver.ForceCameraPosition(other.transform.position, cameraAActiver.transform.rotation);
                }

                // On monte la priorit� de cette cam�ra pour que le Cinemachine Brain effectue la transition
                cameraAActiver.Priority.Value = prioriteActive;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quand le joueur sort de la zone, on remet la priorit� par d�faut (inactive)
        if (other.CompareTag(playerTag))
        {
            if (cameraAActiver != null)
            {
                cameraAActiver.Priority.Value = proriteInactive;
            }
        }
    }
}