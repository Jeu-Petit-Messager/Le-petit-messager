using UnityEngine;
using Unity.Cinemachine; // API Cinemachine v3

public class CameraRecentrer : MonoBehaviour
{
    [Header("Configuration des Cam�ras")]
    [SerializeField] private CinemachineCamera pivotCamera; // Glissez la cam�ra Pivot ici
    [SerializeField] private CinemachineCamera fixedCamera; // Glissez la cam�ra Fixe ici

    private void OnTriggerEnter(Collider other)
    {
        // On v�rifie si c'est le joueur qui entre dans la zone pivot
        if (other.CompareTag("Player"))
        {
            pivotCamera.Priority = 20; // Devient prioritaire
            fixedCamera.Priority = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quand le joueur sort de la zone et revient dans le reste de la rue
        if (other.CompareTag("Player"))
        {
            fixedCamera.Priority = 20; // La cam�ra fixe reprend le contr�le
            pivotCamera.Priority = 10;
        }
    }
}