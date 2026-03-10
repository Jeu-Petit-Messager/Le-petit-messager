using UnityEngine;
using UnityEngine.SceneManagement;

public class nouvellePartie : MonoBehaviour
{
    public void OnNouvellePartieClicked()
    {
        SceneManager.sceneLoaded += LoadedScene;
        SceneManager.LoadScene("sceneJeu");
    }

    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "sceneJeu") return;

        // Trouver et désactiver la caméra d'intro, puis activer la caméra de jeu
        DesactiveCameraIntro camManager = FindObjectOfType<DesactiveCameraIntro>();
        if (camManager != null)
        {
            camManager.ChangeACameraJeu();
        }

        SceneManager.sceneLoaded -= LoadedScene;
    }
}