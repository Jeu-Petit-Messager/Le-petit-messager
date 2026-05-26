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

    [Header("Skip")]
    public GameObject boutonSkip;

    private bool videoSkip = false;

    // Méthode appelée lors du clic sur le bouton "Nouvelle Partie"
    public void OnNouvellePartieClicked()
    {
        PlayerPrefs.DeleteKey("SauvegardeScene");

        // supprimer résultat fin
        PlayerPrefs.DeleteKey("BonFin");

        StartCoroutine(SequenceIntro());
    }
    void Update()
    {
        // touche ESC pour skip
        if (videoObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SkipVideo();
        }
    }

    // bouton skip
    public void SkipVideo()
    {
        videoSkip = true;
    }

   IEnumerator SequenceIntro()
    {
        // activer fade
        fadeCanvas.SetActive(true);

        // écran devient noir
        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);

        // préparer vidéo
        videoPlayer.Prepare();

        // attendre préparation
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        // activer vidéo
        videoObject.SetActive(true);

        // afficher bouton skip
        if (boutonSkip != null)
        {
            boutonSkip.SetActive(true);
        }

        // jouer vidéo
        videoPlayer.Play();

        // cacher fade
        fadeCanvas.SetActive(false);

        // attendre fin vidéo OU skip
        while (videoPlayer.isPlaying)
        {
            // skip avec bouton ou ESC
            if (videoSkip)
            {
                break;
            }

            yield return null;
        }

        // cacher bouton skip
        if (boutonSkip != null)
        {
            boutonSkip.SetActive(false);
        }

        // activer fade PAR DESSUS la vidéo
        fadeCanvas.SetActive(true);

        // lancer fade noir UNE SEULE FOIS
        fadeAnimator.SetTrigger("FadeOut");

        // attendre animation
        yield return new WaitForSeconds(1.5f);

        // charger scène
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