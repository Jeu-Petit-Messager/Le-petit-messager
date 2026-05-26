using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class nouvellePartie : MonoBehaviour
{
    [Header("Fade")]
    public GameObject fadeCanvas;
    public Animator fadeAnimator;

    [Header("Video")]
    public GameObject videoObject;

    public VideoPlayer videoPlayer;
    // Méthode appelée lors du clic sur le bouton "Nouvelle Partie"
    public void OnNouvellePartieClicked()
    {
        PlayerPrefs.DeleteKey("SauvegardeScene");

        // supprimer résultat fin
        PlayerPrefs.DeleteKey("BonFin");

        StartCoroutine(SequenceIntro());
    }

    IEnumerator SequenceIntro()
    {
        // activer fade
        fadeCanvas.SetActive(true);

        // écran devient noir
        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);
        // montrer vidéo
        videoObject.SetActive(true);
        // jouer vidéo
        videoPlayer.Play();

        // cacher image noire
        fadeCanvas.SetActive(false);        

        // attendre vidéo
        yield return new WaitForSeconds(10f);

        // remettre fade
        fadeCanvas.SetActive(true);

        // fade vers jeu
        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1.5f);

        // charger jeu
        LoadSceneJeu();
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