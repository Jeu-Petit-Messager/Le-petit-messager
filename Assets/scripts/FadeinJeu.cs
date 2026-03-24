using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class FadeinJeu : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject fadeCanvas;
    public GameObject videoCanvas;
    public Animator fadeAnimator;
    public VideoPlayer videoPlayer;

    // Méthode pour commencer la séquence de fade
    public void CommenceFade()
    {
        StartCoroutine(Sequence());
    }
    // Coroutine pour gérer la séquence de fade
    IEnumerator Sequence()
    {
        // Activer le fade
        fadeCanvas.SetActive(true);

        // FadeOut (écran devient noir)
        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);

        // Cacher le menu
        menuCanvas.SetActive(false);

        // Lancer la vidéo
        videoCanvas.SetActive(true);
        videoPlayer.Play();

        // Attendre la fin de la vidéo
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // FadeIn (montrer le jeu)
        fadeAnimator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1.5f);

        // Désactiver le fade
        fadeCanvas.SetActive(false);
    }
}