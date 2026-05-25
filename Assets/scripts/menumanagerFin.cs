using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menumanagerFin : MonoBehaviour
{
    public GameObject menu;
    public GameObject quitter;
    public GameObject rejouer;
    public GameObject canvasFade;
    public Animator fadeAnimator;

    public GameObject[] hoverImages;

    void Start()
    {
        menu.SetActive(true);
        quitter.SetActive(false);
    }

    void Update()
    {        
        // Permet de revenir au menu principal en appuyant sur la touche "Echap" depuis les sous-menus
        if (quitter.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }
    }
    
    public void sceneintro()
    {
        SceneManager.LoadScene("sceneIntro");
    }
    // Méthodes pour ouvrir l'interface de quitter le jeu
    public void OpenQuitter()
    {
        ResetHover();

        menu.SetActive(false);
        quitter.SetActive(true);
    }
    // Méthode pour quitter le jeu
    public void Rejouer()
    {
       StartCoroutine(RejouerAvecFade());
    }
     IEnumerator RejouerAvecFade()
    {
        // activer fade
        if (canvasFade != null)
            canvasFade.SetActive(true);

        // animation fade
        if (fadeAnimator != null)
            fadeAnimator.SetTrigger("FadeIn");

        // attendre animation
        yield return new WaitForSeconds(1f);

        // scene actuelle
        string sceneActuelle =
            SceneManager.GetActiveScene().name;

        // si mauvaise fin
        if (sceneActuelle == "sceneMauvaiseFin")
        {
            SceneManager.LoadScene("sceneJeuJour");
        }

        // si scene rejouer
        else if (sceneActuelle == "sceneRejouer")
        {
            SceneManager.LoadScene("sceneJeuNuit");
        }
    }
    // Méthode pour revenir au menu principal depuis les sous-menus
    public void RetourMenu()
    {
        quitter.SetActive(false);
        menu.SetActive(true);
    }
    // Méthode pour réinitialiser les images de survol (hover) en les désactivant
    void ResetHover()
    {
        foreach (GameObject img in hoverImages)
        {
            img.SetActive(false);
        }
    }
}