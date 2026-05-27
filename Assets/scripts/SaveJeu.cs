using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveJeu : MonoBehaviour
{
    [Header("Fade")]
    public Animator fadeAnimator;

    [Header("Audio")]
    public AudioSource sonClick;
    public AudioSource musiqueBackground;

    // BOUTON CONTINUER
    public void Continuer()
    {
        // vérifier sauvegarde
        if (PlayerPrefs.HasKey("SauvegardeScene"))
        {
            // jouer son click
            sonClick.Play();

            // arrêter musique menu
            musiqueBackground.Stop();

            StartCoroutine(ContinuerAvecFade());
        }
    }

    IEnumerator ContinuerAvecFade()
    {
        // activer canvas fade
        fadeAnimator.gameObject.SetActive(true);

        // lancer animation
        fadeAnimator.SetTrigger("FadeOut");

        // attendre animation
        yield return new WaitForSeconds(2.5f);

        // charger sauvegarde
        string sceneSauvegarde =
            PlayerPrefs.GetString("SauvegardeScene");

        SceneManager.LoadScene(sceneSauvegarde);
    }
    // SAUVEGARDE AUTOMATIQUE
    public static void SauvegarderScene()
    {
        PlayerPrefs.SetString(
            "SauvegardeScene",
            "sceneJeuNuit"
        );

        PlayerPrefs.Save();

        Debug.Log("Sauvegarde : sceneJeuNuit");
    }
}