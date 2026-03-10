using UnityEngine;

public class DesactiveCameraIntro : MonoBehaviour
{
    public Camera introCamera;
    public Camera jeuCamera;

    public void ChangeACameraJeu()
    {
        if (introCamera != null) introCamera.gameObject.SetActive(false);
        if (jeuCamera != null) jeuCamera.gameObject.SetActive(true);
    }

    private void TrouveCameras()
    {
        if (introCamera == null) introCamera = GameObject.FindWithTag("IntroCamera")?.GetComponent<Camera>();
        if (jeuCamera == null) jeuCamera = GameObject.FindWithTag("GameCamera")?.GetComponent<Camera>();
    }
}