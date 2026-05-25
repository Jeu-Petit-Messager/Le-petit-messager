using UnityEngine;
using Unity.Cinemachine; // API Cinemachine v3

public class CameraZone : MonoBehaviour
{
    [Header("Configuration des Caméras")]
    [SerializeField] private CinemachineCamera pivotCamera; // Glissez la caméra Pivot ici
    [SerializeField] private CinemachineCamera fixedCamera; // Glissez la caméra Fixe ici

    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est le joueur qui entre dans la zone pivot
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
            fixedCamera.Priority = 20; // La caméra fixe reprend le contrôle
            pivotCamera.Priority = 10;
        }
    }
}