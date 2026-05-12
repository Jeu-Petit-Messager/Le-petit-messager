using UnityEngine;
using UnityEngine.SceneManagement;

public class menumanagerFin : MonoBehaviour
{
    public GameObject menu;
    public GameObject quitter;

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