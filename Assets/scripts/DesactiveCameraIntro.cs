using UnityEngine;

public class DesactiveCameraIntro : MonoBehaviour
{
    public Camera introCamera;
    public Camera jeuCamera;
    
    // Méthode pour désactiver la caméra d'intro et activer la caméra de jeu
    public void ChangeACameraJeu()
    {
        if (introCamera != null) introCamera.gameObject.SetActive(false);
        if (jeuCamera != null) jeuCamera.gameObject.SetActive(true);
    }

    // Méthode pour trouver les caméras si elles ne sont pas assignées dans l'inspecteur
    private void TrouveCameras()
    {
        if (introCamera == null) introCamera = GameObject.FindWithTag("IntroCamera")?.GetComponent<Camera>();
        if (jeuCamera == null) jeuCamera = GameObject.FindWithTag("GameCamera")?.GetComponent<Camera>();
    }
}