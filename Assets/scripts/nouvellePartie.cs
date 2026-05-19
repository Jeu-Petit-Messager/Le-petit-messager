using UnityEngine;
using UnityEngine.SceneManagement;

public class nouvellePartie : MonoBehaviour
{
    // Méthode appelée lors du clic sur le bouton "Nouvelle Partie"
    public void OnNouvellePartieClicked()
    {
        CancelInvoke(("LoadSceneJeu"));
        Invoke(("LoadSceneJeu"), 4f);
    }

    private void LoadSceneJeu()
    {
        SceneManager.sceneLoaded += LoadedScene;
        SceneManager.LoadScene("sceneJeuJour");
    }

    // Méthode appelée lorsque la scène est chargée
    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "sceneJeuJour") return;

        // Trouver et désactiver la caméra d'intro, puis activer la caméra de jeu
        DesactiveCameraIntro camManager = FindObjectOfType<DesactiveCameraIntro>();
        if (camManager != null)
        {
            camManager.ChangeACameraJeu();
        }
        // Désabonner l'événement pour éviter les appels multiples
        SceneManager.sceneLoaded -= LoadedScene;
    }
}