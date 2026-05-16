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
        fadeAnimator.SetTrigger("FadeIn");

        // attendre animation
        yield return new WaitForSeconds(1f);

        // charger sauvegarde
        string sceneSauvegarde =
            PlayerPrefs.GetString("SauvegardeScene");

        SceneManager.LoadScene(sceneSauvegarde);
    }

    // BOUTON NOUVELLE PARTIE
    public void NouvellePartie()
    {
        // supprimer sauvegarde
        PlayerPrefs.DeleteKey("SauvegardeScene");
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